using Questao5.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Input
{
    public class MovimentacaoInput
    {
        [Required]
        public string IdRequisicao { get; set; }
        [Required]
        public int NumeroConta { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0")]
        public double Valor { get; set; }
        [Required]
        public TipoMovimentacao Tipo { get; set; }
    }
}
