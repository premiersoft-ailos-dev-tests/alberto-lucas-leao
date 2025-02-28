using Questao5.Application.Commands.Requests;
using Xunit;

namespace Questao5.Testes.ApplicationTests.CommandsTests.RequestTests;

[Collection(nameof(MovimentacaoRequestCommandCollection))]
public class MovimentacaoRequestCommandTests
{
    readonly MovimentacaoRequestCommandFixture _movimentacaoRequestCommandFixture;

    public MovimentacaoRequestCommandTests(MovimentacaoRequestCommandFixture movimentacaoRequestCommandFixture)
    {
        _movimentacaoRequestCommandFixture = movimentacaoRequestCommandFixture;
    }

    [Fact(DisplayName = "MovimentacaoRequestCommand Valido")]
    [Trait("Categoria", "MovimentacaoCommand")]
    public async void MovimentacaoRequestCommand_Valido() 
    {
        //Arrange
        var input = _movimentacaoRequestCommandFixture.GerarMovimentacaoRequestCommand();

        //Act
        var movimentacao = new MovimentacaoRequestCommand(input.IdRequisicao,
                                                          input.NumeroConta,
                                                          input.Valor,
                                                          input.Tipo);

        //Assert
        Assert.Equal(input.IdRequisicao, movimentacao.IdRequisicao);
    }
}
