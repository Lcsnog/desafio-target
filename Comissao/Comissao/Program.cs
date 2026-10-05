using System.Text.Json;
using VendasJson.Models;
using VendasJson.Services;

namespace Program
{
    class Program
    {
        static void Main()
        {
            ComissaoService comissaoService = new ComissaoService();

            string jsonString = File.ReadAllText("vendas.json");

            DadosVendas dados = JsonSerializer.Deserialize<DadosVendas>( 
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!; 

            Console.WriteLine($"Quantidade de vendas: {dados.Vendas.Count}\n");

            foreach (var venda in dados.Vendas)
            {
                Console.Write($"Vendedor: {venda.Vendedor}");
                Console.Write($"Valor: R$ {venda.Valor:F2}");

                decimal comissao =
                    comissaoService.CalcularComissao(venda.Valor);

                Console.WriteLine($"Comissão: R$ {comissao:F2}");
            }
        }
    }
}