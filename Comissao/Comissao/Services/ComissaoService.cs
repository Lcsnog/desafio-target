using System.Threading.Channels;
using VendasJson.Models;

namespace VendasJson.Services
{
    public class ComissaoService
    {
        public decimal CalcularComissao(decimal valorVenda)
        {

            if (valorVenda < 100)
            {
                return 0;
            }
            
            else if (valorVenda < 500)
            {
                return valorVenda * 0.01m;
            }

            else
            {
                return valorVenda * 0.05m;
            }

        }
    }
}