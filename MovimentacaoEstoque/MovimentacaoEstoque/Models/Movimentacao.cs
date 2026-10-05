namespace ControleEstoque.Models;

public class Movimentacao
{
    public int Id { get; set; }
    public int CodigoProduto { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public int EstoqueFinal { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;
}