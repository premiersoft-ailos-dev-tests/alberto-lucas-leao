using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Testes.ApplicationTests.DomainTests.EntitiTests
{
    public class IdempotenciaTests
    {
        [Fact(DisplayName = "Idempotencia Valido")]
        [Trait("Categoria", "Movimentacao")]
        public async void Idepotencia_Valido()
        {
            //Arrange
            var ChaveIdempotencia = "6fc521cd-ebd6-4596-84b3-e7a143eaaf23";
            var Requisicao = "/Request/";
            var Resultado = "/Response/";

            //Act
            var saldo = new Idempotencia
            {
                ChaveIdempotencia = ChaveIdempotencia,
                Requisicao = Requisicao,
                Resultado = Resultado
            };

            //Assert
            Assert.Equal(ChaveIdempotencia, saldo.ChaveIdempotencia);
            Assert.Equal(Requisicao, saldo.Requisicao);
            Assert.Equal(Resultado, saldo.Resultado);
        }
    }
}
