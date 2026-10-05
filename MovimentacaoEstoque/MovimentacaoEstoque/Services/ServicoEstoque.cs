using ControleEstoque.Models;

namespace ControleEstoque.Services;

public class ServicoEstoque
{
    private readonly List<Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = new();

    private int _proximoId = 1;

    public ServicoEstoque(List<Produto> produtos)
    {
        _produtos = produtos;
    }

    public IReadOnlyList<Produto> Produtos => _produtos;

    public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

    public Produto? BuscarProduto(int codigo)
    {
        return _produtos.FirstOrDefault(p => p.Codigo == codigo);
    }

    public Movimentacao Movimentar(
        int codigo,
        TipoMovimentacao tipo,
        int quantidade,
        string descricao)
    {
        var produto = BuscarProduto(codigo)
            ?? throw new InvalidOperationException(
                $"Produto {codigo} não encontrado.");

        if (quantidade <= 0)
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");

        if (tipo == TipoMovimentacao.Saida &&
            quantidade > produto.Estoque)
        {
            throw new InvalidOperationException(
                $"Estoque insuficiente. " +
                $"Disponível: {produto.Estoque}, " +
                $"solicitado: {quantidade}.");
        }

        produto.Estoque += tipo == TipoMovimentacao.Entrada
            ? quantidade
            : -quantidade;

        var movimentacao = new Movimentacao
        {
            Id = _proximoId++,
            CodigoProduto = codigo,
            Tipo = tipo,
            Descricao = descricao,
            Quantidade = quantidade,
            EstoqueFinal = produto.Estoque
        };

        _movimentacoes.Add(movimentacao);

        return movimentacao;
    }
}