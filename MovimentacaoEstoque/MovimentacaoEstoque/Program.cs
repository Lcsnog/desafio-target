using System.Text.Json;
using ControleEstoque.Models;
using ControleEstoque.Services;

namespace ControleEstoque;

public class Program
{
    private const string JsonEstoque = """
    {
      "estoque": [
        { "codigoProduto": 101, "descricaoProduto": "Caneta Azul", "estoque": 150 },
        { "codigoProduto": 102, "descricaoProduto": "Caderno Universitário", "estoque": 75 },
        { "codigoProduto": 103, "descricaoProduto": "Borracha Branca", "estoque": 200 },
        { "codigoProduto": 104, "descricaoProduto": "Lápis Preto HB", "estoque": 320 },
        { "codigoProduto": 105, "descricaoProduto": "Marcador de Texto Amarelo", "estoque": 90 }
      ]
    }
    """;

    public static void Main()
    {
        var dados = JsonSerializer.Deserialize<EstoqueWrapper>(JsonEstoque)!;

        var servico = new ServicoEstoque(dados.Estoque);

        while (true)
        {
            Console.WriteLine("\n===== CONTROLE DE ESTOQUE =====");
            Console.WriteLine("1 - Listar estoque");
            Console.WriteLine("2 - Lançar movimentação");
            Console.WriteLine("3 - Histórico de movimentações");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ListarEstoque(servico);
                    break;

                case "2":
                    LancarMovimentacao(servico);
                    break;

                case "3":
                    ListarHistorico(servico);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private static void ListarEstoque(ServicoEstoque servico)
    {
        Console.WriteLine("\nCód  | Produto                      | Estoque");
        Console.WriteLine("-----|------------------------------|--------");

        foreach (var produto in servico.Produtos)
        {
            Console.WriteLine(
                $"{produto.Codigo,-4} | " +
                $"{produto.Descricao,-28} | " +
                $"{produto.Estoque}");
        }
    }

    private static void LancarMovimentacao(ServicoEstoque servico)
    {
        ListarEstoque(servico);

        Console.Write("\nCódigo do produto: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido.");
            return;
        }

        Console.Write("Tipo (1 = Entrada, 2 = Saída): ");

        if (!int.TryParse(Console.ReadLine(), out int tipoInt) ||
            (tipoInt != 1 && tipoInt != 2))
        {
            Console.WriteLine("Tipo inválido.");
            return;
        }

        Console.Write("Quantidade: ");

        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        Console.Write(
            "Descrição da movimentação " +
            "(ex.: Compra, Venda, Devolução): ");

        string descricao = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("A descrição é obrigatória.");
            return;
        }

        try
        {
            var tipo = (TipoMovimentacao)tipoInt;

            var movimentacao = servico.Movimentar(
                codigo,
                tipo,
                quantidade,
                descricao);

            var produto = servico.BuscarProduto(codigo)!;

            Console.Write("\nMovimentação registrada!\n");
            Console.WriteLine($"ID da movimentação: {movimentacao.Id}");
            Console.WriteLine($"Tipo: {movimentacao.Tipo}");
            Console.WriteLine($"Descrição: {movimentacao.Descricao}");
            Console.WriteLine($"Produto: {produto.Descricao}");
            Console.WriteLine(
                $"Quantidade final em estoque: " +
                $"{movimentacao.EstoqueFinal}");
        }
        catch (Exception ex)
        {
            Console.Write($"\nErro: {ex.Message}\n");
        }
    }

    private static void ListarHistorico(ServicoEstoque servico)
    {
        if (servico.Movimentacoes.Count == 0)
        {
            Console.Write("\nNenhuma movimentação lançada.\n");
            return;
        }

        Console.WriteLine(
            "\nID | Data             | Prod | Tipo    | " +
            "Qtde | Final | Descrição");

        foreach (var movimentacao in servico.Movimentacoes)
        {
            Console.WriteLine(
                $"{movimentacao.Id,-2} | " +
                $"{movimentacao.Data:dd/MM/yy HH:mm} | " +
                $"{movimentacao.CodigoProduto,-4} | " +
                $"{movimentacao.Tipo,-7} | " +
                $"{movimentacao.Quantidade,-4} | " +
                $"{movimentacao.EstoqueFinal,-5} | " +
                $"{movimentacao.Descricao}");
        }
    }
}