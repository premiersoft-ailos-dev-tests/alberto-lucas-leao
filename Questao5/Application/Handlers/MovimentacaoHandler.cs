using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovimentacaoHandler : IRequestHandler<MovimentacaoRequestCommand, string>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoHandler(IContaCorrenteRepository contaCorrenteRepository,
                                   IMovimentacaoRepository movimentacaoRepository,
                                   IHttpContextAccessor httpContextAccessor)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentException(nameof(IContaCorrenteRepository));
            _movimentacaoRepository = movimentacaoRepository ?? throw new ArgumentException(nameof(IMovimentacaoRepository));
        }

        public async Task<string> Handle(MovimentacaoRequestCommand request, CancellationToken cancellationToken)
        {
           
            var contaCorrente = await _contaCorrenteRepository.ObterContaCorrete(request.NumeroConta, cancellationToken);
            if (contaCorrente == null || contaCorrente.Ativo == 0) 
                throw new BadHttpRequestException("Não foi possível obter a conta correte, verifique se a conta esta ativa");            

            var movimentacaoContaCorrete = await _movimentacaoRepository.RegistrarMovimentacao(
                new Movimentacao
                {
                    IdContaCorrente = contaCorrente.IdContaCorrente,
                    DataMovimento = DateTime.Now,
                    TipoMovimento = request.Tipo.ToString(),
                    Valor = Math.Round(request.Valor, 2)
                },
                cancellationToken
            );            

            return movimentacaoContaCorrete.IdMovimento;            
        }
    }
}
