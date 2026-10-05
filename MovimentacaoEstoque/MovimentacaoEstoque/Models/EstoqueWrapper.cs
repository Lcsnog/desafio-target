using System.Text.Json.Serialization;

namespace ControleEstoque.Models;

public class EstoqueWrapper
{
    [JsonPropertyName("estoque")]
    public List<Produto> Estoque { get; set; } = new();
}