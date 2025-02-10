using Bogus;
using Moq.AutoMock;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Testes.Handlers
{
    [CollectionDefinition(nameof(SaldoContaCorreteHandlerCollection))]
    public class SaldoContaCorreteHandlerCollection : ICollectionFixture<SaldoContaCorreteHandlerFixture> { }

    public class SaldoContaCorreteHandlerFixture : IDisposable
    {
        public SaldoContaCorreteHandler SaldoContaCorreteHandler;
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

        public List<Movimentacao> GerarMovimentacoes() 
        {
            var movimentacao = new Faker<Movimentacao>(_localizacao)
                .RuleFor(x => x.IdMovimento, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.IdContaCorrente, f => f.Random.Number(1, 37).ToString())
                .RuleFor(x => x.DataMovimento, f => f.Date.Past())
                .RuleFor(x => x.TipoMovimento, f => "C")
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 100));

            return movimentacao.Generate(3);
        }

        public SaldoContaCorrenteRequestCommand GerarSaldoContaCorrenteRequestCommand() 
        {
            return new Faker<SaldoContaCorrenteRequestCommand>(_localizacao)
                .RuleFor(x => x.NumeroContaCorrete, f => f.Random.Number(1, 10));
        }

        public SaldoContaCorreteHandler ObterSaldoContaCorreteHandler()
        {
            Mocker = new AutoMocker();
            SaldoContaCorreteHandler = Mocker.CreateInstance<SaldoContaCorreteHandler>();

            return SaldoContaCorreteHandler;
        }

        public void Dispose() { }
    }
}
