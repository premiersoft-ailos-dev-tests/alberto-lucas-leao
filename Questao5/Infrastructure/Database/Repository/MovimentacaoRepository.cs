using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Questao5.Infrastructure.Database.Repository.Querys;
using StackExchange.Redis;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        private readonly IDatabase _redusDb;


        public MovimentacaoRepository(DatabaseConfig databaseConfig, IConnectionMultiplexer redis)
        {
            this.databaseConfig = databaseConfig;

            _redusDb = redis.GetDatabase();
        }

        public async Task<List<Movimentacao>> ObterMovimentacaoContaCorrete(string idContaCorrente, CancellationToken cancellationToken) 
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var movimentacoes = await connection.QueryAsync<Movimentacao>(
                new CommandDefinition(
                    MovimentacaoQuery.ObterMovimentacoesContaCorrete, 
                    new { IdContaCorrente = idContaCorrente }, 
                    cancellationToken: cancellationToken
                )
            );

            if (movimentacoes.Count() == 0) return new();

            List<Movimentacao> Results = movimentacoes.ToList();            

            return Results;
        }

        public async Task<Movimentacao> RegistrarMovimentacao(Movimentacao movimentacao, CancellationToken cancellationToken) 
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync(cancellationToken);
            using var transaction = connection.BeginTransaction();

            try
            {
                var id = Guid.NewGuid().ToString();
                var data = movimentacao.DataMovimento.ToString();

                await connection.QueryAsync<long>(
                    new CommandDefinition(
                        MovimentacaoQuery.AdicionarMovimentacao,
                        new
                        {
                            IdMovimento = id,
                            IdContaCorrente = movimentacao.IdContaCorrente,
                            DataMovimento = movimentacao.DataMovimento.ToString(),
                            TipoMovimento = movimentacao.TipoMovimento,
                            Valor = movimentacao.Valor
                        },
                        cancellationToken: cancellationToken
                    )
                );

                var result = await connection.QueryAsync<Movimentacao>(MovimentacaoQuery.ObterMovimentacao, new { IdMovimento = id });

                var redisKey = $"saldo:{movimentacao.IdContaCorrente}";
                var saldoAtual = Convert.ToDouble(await _redusDb.StringGetAsync(redisKey));
                saldoAtual += movimentacao.TipoMovimento.ToString() == "C" ? movimentacao.Valor : -movimentacao.Valor;
                await _redusDb.StringSetAsync(redisKey, saldoAtual);

                transaction.Commit();

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }
            
        }

    }
}
