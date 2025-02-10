using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        public Task<ContaCorrete> ObterContaCorrete(int numero, CancellationToken cancellationToken);
    }
}
