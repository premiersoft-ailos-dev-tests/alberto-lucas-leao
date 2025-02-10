using Moq;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Testes.Handlers
{
    [Collection(nameof(MovimentacaoHandlerCollection))]
    public class MovimentacaoHandlerTeste
    {
        readonly MovimentacaoHandlerFixture _movimentacaoHandlerFixture;
        private readonly MovimentacaoHandler _movimentacaoHandler;

        public MovimentacaoHandlerTeste(MovimentacaoHandlerFixture movimentacaoHandlerFixture)
        {
            _movimentacaoHandlerFixture = movimentacaoHandlerFixture;
            _movimentacaoHandler = _movimentacaoHandlerFixture.ObterMovimentacaoHandler();
        }

        [Fact(DisplayName = "MovimentacaoHandler Valido")]
        [Trait("Categoria", "MovimentacaoHandler")]
        public async void MovimentacaoHandler_Valido()
        {
            //Arrange
            var contaCorrente = _movimentacaoHandlerFixture.GerarContaCorrete();
            var movimentacao = _movimentacaoHandlerFixture.GerarMovimentacao();
            var movimentacaoRequest = _movimentacaoHandlerFixture.GerarMovimentacaoRequestCommand();

            var contaCorrenteRepository = _movimentacaoHandlerFixture.Mocker.GetMock<IContaCorrenteRepository>();
            _ = contaCorrenteRepository.Setup(c => c.ObterContaCorrete(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(contaCorrente);

            var movimentacaoRepository = _movimentacaoHandlerFixture.Mocker.GetMock<IMovimentacaoRepository>();
            _ = movimentacaoRepository.Setup(c => c.RegistrarMovimentacao(It.IsAny<Movimentacao>(), CancellationToken.None)).ReturnsAsync(movimentacao);

            //Act
            var result = await _movimentacaoHandler.Handle(movimentacaoRequest, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }
    }
}
