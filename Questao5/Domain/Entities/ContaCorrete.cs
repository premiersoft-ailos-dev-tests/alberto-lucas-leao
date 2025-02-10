using System.Text.Json.Serialization;

namespace Questao5.Domain.Entities
{
    public class ContaCorrete
    {
        [JsonPropertyName("idcontacorrente")]
        public string IdContaCorrente { get; set; } = string.Empty;
        [JsonPropertyName("numero")]
        public int Numero { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("ativo")]
        public int Ativo { get; set;}
    }
}
