using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Shared
{
    public static class MailConstants
    {
        public const string BuyMessage = "O preço do ativo atingiu o ponto de compra";
        public const string SellMessage = "O preço do ativo atingiu o ponto de venda";
    }
}
