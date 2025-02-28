using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Redis.Querys;
using Questao5.Infrastructure.Sqlite;
using StackExchange.Redis;

namespace Questao5.Infrastructure.Redis
{
    public class RedisService : BackgroundService
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly IDatabase _redusDb;

        public RedisService(DatabaseConfig databaseConfig, IConnectionMultiplexer redis)
        {
            this.databaseConfig = databaseConfig;
            _redusDb = redis.GetDatabase();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                var contas = await connection.QueryAsync<string>(RedisQuery.ObterIdContaMovimentacao);

                foreach (var idConta in contas) 
                {
                    var saldo = await connection.ExecuteScalarAsync<double>(
                        RedisQuery.ObterMovimentacaoConta,
                        new { IdConta = idConta });

                    await _redusDb.StringSetAsync($"saldo:{idConta}", saldo);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
