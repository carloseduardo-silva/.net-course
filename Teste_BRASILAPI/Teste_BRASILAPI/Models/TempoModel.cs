using System.Text.Json.Serialization;

namespace Teste_BRASILAPI.Models
{
    public class TempoModel
    {
        [JsonPropertyName("Cidade")]
        public string? Cidade { get; set; }

        [JsonPropertyName("Estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("Data de Atualização")]
        public string? AtualizacaoData { get; set; }
        
        [JsonPropertyName("Previsao")]
        public Clima Previsao { get; set; }

        public bool Verificacao { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class Clima
    {
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        [JsonPropertyName("condicao")]
        public string? Condicao { get; set; }

        [JsonPropertyName("min")]
        public int Min { get; set; }

        [JsonPropertyName("max")]
        public int Max { get; set; }

        [JsonPropertyName("indice_uv")]
        public int IndiceUv { get; set; }

        [JsonPropertyName("condicao_desc")]
        public string? CondicaoDesc { get; set; }
    }
}
