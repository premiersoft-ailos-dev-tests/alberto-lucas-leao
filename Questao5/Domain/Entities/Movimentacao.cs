using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimentacao
    {
        public string IdMovimento { get; set; } = string.Empty;
        public string IdContaCorrente { get; set; } = string.Empty;
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; } = string.Empty;
        public double Valor { get; set; }
    }
}
