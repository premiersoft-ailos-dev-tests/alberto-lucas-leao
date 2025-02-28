using Questao5.Domain.Interfaces;
using StackExchange.Redis;

namespace Questao5.Infrastructure.Database.Repository
{
    public class SaldoRepository : ISaldoRepository
    {
        private readonly IDatabase _redisDb;

        public SaldoRepository(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<double> ObterSaldo(string id) 
        {
            string redisKey = $"saldo:{id}";
            var saldo = await _redisDb.StringGetAsync(redisKey);

            return saldo.HasValue ? (double)saldo : 0.00;
        }
    }
}
