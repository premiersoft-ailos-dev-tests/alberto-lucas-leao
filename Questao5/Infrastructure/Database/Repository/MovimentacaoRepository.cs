using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Questao5.Infrastructure.Database.Repository.Querys;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentacaoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
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
            
            var id = Guid.NewGuid().ToString();

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

            var result = await connection.QueryAsync<Movimentacao>(MovimentacaoQuery.ObterMovimentacao, new {IdMovimento = id });            

            return result.FirstOrDefault();
        }

    }
}
