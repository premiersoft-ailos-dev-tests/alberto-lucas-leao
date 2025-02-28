using Moq;
using Questao5.Application.Handlers;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Testes.ApplicationTests.HandlersTests.SaldoTests
{
    [Collection(nameof(SaldoContaCorreteHandlerCollection))]
    public class SaldoContaCorreteHandlerTeste
    {
        readonly SaldoContaCorreteHandlerFixture _saldoContaCorreteHandlerFixture;
        private readonly SaldoHandler _saldoContaCorreteHandler;

        public SaldoContaCorreteHandlerTeste(SaldoContaCorreteHandlerFixture saldoContaCorreteHandlerFixture)
        {
            _saldoContaCorreteHandlerFixture = saldoContaCorreteHandlerFixture;
            _saldoContaCorreteHandler = _saldoContaCorreteHandlerFixture.ObterSaldoContaCorreteHandler();
        }

        [Fact(DisplayName = "SaldoContaCorreteHandler Valido")]
        [Trait("Categoria", "SaldoContaCorreteHandler")]
        public async void SaldoContaCorreteHandler_Valido()
        {
            //Arrange
            var contaCorrente = _saldoContaCorreteHandlerFixture.GerarContaCorrete();
            var movimentacoes = _saldoContaCorreteHandlerFixture.GerarMovimentacoes();
            var movimentacaoRequest = _saldoContaCorreteHandlerFixture.GerarSaldoContaCorrenteRequestCommand();

            var contaCorrenteRepository = _saldoContaCorreteHandlerFixture.Mocker.GetMock<IContaCorrenteRepository>();
            _ = contaCorrenteRepository.Setup(c => c.ObterContaCorrete(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(contaCorrente);

            var movimentacaoRepository = _saldoContaCorreteHandlerFixture.Mocker.GetMock<IMovimentacaoRepository>();
            _ = movimentacaoRepository.Setup(c => c.ObterMovimentacaoContaCorrete(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(movimentacoes);

            //Act
            var result = await _saldoContaCorreteHandler.Handle(movimentacaoRequest, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }
    }
}
