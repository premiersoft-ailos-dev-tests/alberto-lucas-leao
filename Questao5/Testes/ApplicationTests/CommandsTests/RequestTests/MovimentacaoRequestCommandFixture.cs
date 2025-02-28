using Bogus;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Testes.ApplicationTests.HandlersTests.MovimentacaoTests;
using Xunit;

namespace Questao5.Testes.ApplicationTests.CommandsTests.RequestTests
{
    [CollectionDefinition(nameof(MovimentacaoRequestCommandCollection))]
    public class MovimentacaoRequestCommandCollection : ICollectionFixture<MovimentacaoRequestCommandFixture> { }
    public class MovimentacaoRequestCommandFixture : IDisposable
    {
        private string _localizacao = "pt_BR";

        public MovimentacaoRequestCommand GerarMovimentacaoRequestCommand()
        {
            return new Faker<MovimentacaoRequestCommand>(_localizacao)
                .RuleFor(x => x.IdRequisicao, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.NumeroConta, f => f.Random.Number(1, 3))
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 100))
                .RuleFor(x => x.Tipo, f => TipoMovimentacao.C);
        }

        public void Dispose() { }
    }
}
