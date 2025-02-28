using Bogus;
using Moq.AutoMock;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Xunit;

namespace Questao5.Testes.ApplicationTests.HandlersTests.MovimentacaoTests
{
    [CollectionDefinition(nameof(MovimentacaoHandlerCollection))]
    public class MovimentacaoHandlerCollection : ICollectionFixture<MovimentacaoHandlerFixture> { }
    public class MovimentacaoHandlerFixture : IDisposable
    {
        public MovimentacaoHandler MovimentacaoHandler;
        public AutoMocker Mocker;
        private string _localizacao = "pt_BR";

        public ContaCorrete GerarContaCorrete()
        {
            return new Faker<ContaCorrete>(_localizacao)
                .RuleFor(x => x.IdContaCorrente, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.Numero, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Nome, f => f.Name.FullName())
                .RuleFor(x => x.Ativo, f => f.Random.Number(1, 2));
        }

        public Movimentacao GerarMovimentacao()
        {
            return new Faker<Movimentacao>(_localizacao)
                .RuleFor(x => x.IdMovimento, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.IdContaCorrente, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.DataMovimento, f => f.Date.Past().ToString())
                .RuleFor(x => x.TipoMovimento, f => "C")
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 100));
        }

        public MovimentacaoRequestCommand GerarMovimentacaoRequestCommand()
        {
            return new Faker<MovimentacaoRequestCommand>(_localizacao)
                .RuleFor(x => x.IdRequisicao, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.NumeroConta, f => f.Random.Number(1, 3))
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 100))
                .RuleFor(x => x.Tipo, f => TipoMovimentacao.C);
        }

        public MovimentacaoHandler ObterMovimentacaoHandler()
        {
            Mocker = new AutoMocker();
            MovimentacaoHandler = Mocker.CreateInstance<MovimentacaoHandler>();

            return MovimentacaoHandler;
        }

        public void Dispose() { }
    }
}
