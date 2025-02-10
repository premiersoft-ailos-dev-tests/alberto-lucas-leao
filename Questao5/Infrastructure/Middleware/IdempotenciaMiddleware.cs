using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Middleware.Querys;
using Questao5.Infrastructure.Sqlite;
using System.Text.Json;

namespace Questao5.Infrastructure.Middleware
{
    public class IdempotenciaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DatabaseConfig _databaseConfig;

        public IdempotenciaMiddleware(RequestDelegate next, DatabaseConfig databaseConfig)
        {
            _next = next;
            _databaseConfig = databaseConfig;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            context.Request.EnableBuffering();

            if (context.Request.Method != HttpMethods.Post) { await _next(context); return; }


            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            string requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            var requestJson = JsonSerializer.Deserialize<JsonElement>(requestBody);
            if (!requestJson.TryGetProperty("idRequisicao", out var idRequisicaoElement))
            {
                await _next(context);
                return;
            }

            string idRequisicao = idRequisicaoElement.GetString();

            using var connection = new SqliteConnection(_databaseConfig.Name);
            await connection.OpenAsync();

            var existingResponse = await connection.QueryFirstOrDefaultAsync<Idempotencia>(IdempotenciaQuery.ObterIdempotencia,
                                                                                     new { IdRequisicao = idRequisicao });

            if (existingResponse != null)
            {
                await context.Response.WriteAsync(existingResponse.Resultado);
                return;
            }

            var originalResponseBody = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalResponseBody);

            await connection.ExecuteAsync(IdempotenciaQuery.SalvarIdempotencia, new
            {
                IdRequisicao = idRequisicao,
                Requisicao = requestBody,
                Resultado = responseBody
            });
            
            context.Response.Body = originalResponseBody;
        }
    }
}
