using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class SaldoHandler : IRequestHandler<SaldoContaCorrenteRequestCommand, SaldoContaCorrenteResponseCommand>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly ISaldoRepository _saldoRepository;

        public SaldoHandler(IContaCorrenteRepository contaCorrenteRepository,
                            IMovimentacaoRepository movimentacaoRepository,
                            ISaldoRepository saldoRepository,
                            IHttpContextAccessor httpContextAccessor)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentException(nameof(IContaCorrenteRepository));
            _movimentacaoRepository = movimentacaoRepository ?? throw new ArgumentException(nameof(IMovimentacaoRepository));
            _saldoRepository = saldoRepository ?? throw new ArgumentException(nameof(ISaldoRepository));
        }

        public async Task<SaldoContaCorrenteResponseCommand> Handle(SaldoContaCorrenteRequestCommand request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.ObterContaCorrete(request.NumeroContaCorrete, cancellationToken);
            if (contaCorrente == null || contaCorrente.Ativo == 0) 
                throw new BadHttpRequestException("Não foi possível obter a conta correte, verifique se a conta esta ativa");

            // TODO: Abordagem obtendo dados do banco Sqlite
            //var movimentacaoContaCorrete = await _movimentacaoRepository.ObterMovimentacaoContaCorrete(contaCorrente.IdContaCorrente, cancellationToken);

            //var saldoContaCorrente = new SaldoContaCorrenteResponseCommand();
            //if (movimentacaoContaCorrete.Count() == 0)
            //{
            //    saldoContaCorrente.MontarObjetoSaldoContaCorrente(contaCorrente, 0.00);
            //    return saldoContaCorrente;
            //}

            //saldoContaCorrente.MontarObjetoSaldoContaCorrente(contaCorrente, CalcularSaldoMovimentacao(movimentacaoContaCorrete));

            //TODO: Abordagem obtendo dados Redis (CQRS)
            var saldoContaCorrente = await _saldoRepository.ObterSaldo(contaCorrente.IdContaCorrente);
            var dadosConta = new SaldoContaCorrenteResponseCommand
            {
                ContaCorrente = contaCorrente.Numero,
                Titular = contaCorrente.Nome,
                DataHoraConsulta = DateTime.Now.ToString(),
                Saldo = Math.Round(saldoContaCorrente, 2)
            };

            return dadosConta;
        }

        public static double CalcularSaldoMovimentacao(List<Movimentacao> movimentacoes) 
        {
            double credito = movimentacoes.Where(m => m.TipoMovimento == TipoMovimentacao.C.ToString()).Sum(m => m.Valor);
            double debito = movimentacoes.Where(m => m.TipoMovimento == TipoMovimentacao.D.ToString()).Sum(m => m.Valor);

            return credito - debito;
        }
    }
}
