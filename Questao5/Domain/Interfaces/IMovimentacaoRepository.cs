using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentacaoRepository
    {
        public Task<List<Movimentacao>> ObterMovimentacaoContaCorrete(string idContaCorrete, CancellationToken cancellationToken);
        public Task<Movimentacao> RegistrarMovimentacao(Movimentacao movimentacao, CancellationToken cancellationToken);
    }
}
