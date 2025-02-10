using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.Repository.Querys;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrete> ObterContaCorrete(int numero, CancellationToken cancellationToken) 
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var contaCorrente = await connection.QueryAsync<ContaCorrete>(
                new CommandDefinition(
                    ContaCorrenteQuery.ObterContaCorrente,
                    new 
                    { 
                        Numero = numero
                    },
                    cancellationToken: cancellationToken
                )
            );

            if (contaCorrente.Count() == 0) return new();

            return contaCorrente.FirstOrDefault();
        }
    }
}
