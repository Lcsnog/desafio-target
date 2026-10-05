using System.Globalization;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Console.Write("Digite um valor: R$");
            double valor = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

            Console.Write("Digite a data de vencimento: ");

            string data = "";

            for (int i = 0; i < 8; i++)
            {
                ConsoleKeyInfo tecla = Console.ReadKey();

                data += tecla.KeyChar;

                if (i == 1 || i == 3)
                {
                    data += "/";
                    Console.Write("/");
                }
            }

            Console.WriteLine();

            DateTime vencimento = DateTime.ParseExact(
                data,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
            );

            DateTime hoje = DateTime.Today;

            TimeSpan diferenca = hoje - vencimento;
            int diasAtraso = diferenca.Days;

            if (diasAtraso > 0)
            {
                double percentualJuros = 0.025;
                double jurosDias = percentualJuros * diasAtraso;
                double jurosValor = valor * percentualJuros * diasAtraso;
                double valorTotal = valor + jurosValor;

                Console.Write("\nResultado: \n");
                Console.Write($"Percentual cobrado por dia atrasado: {percentualJuros:P1}\n");
                Console.Write($"Dias em atraso: {diasAtraso}\n");
                Console.Write($"Percentual de juros: {jurosDias:P2}\n");
                Console.Write($"Valor dos juros em cima do valor: R${jurosValor:F2}\n");
                Console.Write($"Valor total a pagar: R${valorTotal:F2}\n");
            }
            else
            {
                Console.Write("\nO pagamento não está atrasado.\n");
                Console.Write($"Valor a pagar: R$ {valor:F2}\n");
            }

        }
    }

}