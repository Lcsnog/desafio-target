using System.Text.Json.Serialization;

namespace ControleEstoque.Models;

public class Produto
{
    [JsonPropertyName("codigoProduto")]
    public int Codigo { get; set; }

    [JsonPropertyName("descricaoProduto")]
    public string Descricao { get; set; } = string.Empty;

    [JsonPropertyName("estoque")]
    public int Estoque { get; set; }
}