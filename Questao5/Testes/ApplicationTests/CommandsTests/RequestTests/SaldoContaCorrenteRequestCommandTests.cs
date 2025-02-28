using Questao5.Application.Commands.Requests;
using Xunit;

namespace Questao5.Testes.ApplicationTests.CommandsTests.RequestTests
{
    public class SaldoContaCorrenteRequestCommandTests
    {
        [Fact(DisplayName = "MovimentacaoRequestCommand Valido")]
        [Trait("Categoria", "MovimentacaoCommand")]
        public async void MovimentacaoRequestCommand_Valido()
        {
            //Arrange
            int input = 123;

            //Act
            var saldo = new SaldoContaCorrenteRequestCommand(input);

            //Assert
            Assert.Equal(input, saldo.NumeroContaCorrete);
        }
    }
}
