using Microsoft.Extensions.Configuration;
using MonitoraAtivo.Domain.Interfaces;
using MonitoraAtivo.Domain.Models;
using MonitoraAtivo.Domain.Models.Configuration;
using MonitoraAtivo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoraAtivo.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IFinanceService _financeService;
        private readonly IMailService _mailService;
        private readonly ApplicationConfiguration _config;
        private readonly ApplicationArgs _args;

        public ApplicationService(IFinanceService financeService, ApplicationConfiguration config, IMailService mailService, ApplicationArgs args)
        {
            _args = args;
            _financeService = financeService;
            _mailService = mailService;
            _config = config;
        }
        public async void StartApplication()
        {
            
            await Monitorate();
        }

        
        private async Task Monitorate()
        {
            var symbol = _args.Symbol;
            var sellPrice = _args.SellPrice;
            var buyPrice = _args.BuyPrice;
            var title = "Monitoração Ativo - " + symbol;
            while (true)
            {
                Console.WriteLine("Obtendo cotacao ativo " + symbol);
                var response = await _financeService.GetStockData(symbol);
                var lastQuote = response.values[0];
                var lastValue = lastQuote.close;
                Console.WriteLine("Ativo: " + symbol );
                Console.WriteLine("Fechamento: " + lastValue);
                Console.WriteLine("Horario: " + lastQuote.datetime);
                if (lastValue >= sellPrice)
                {
                    var mailResponse= await _mailService.SendEmailAsync(title, MailConstants.SellMessage );
                    if (mailResponse != null)
                    {
                        break;
                    }
                    
                }
                else if (lastValue <= buyPrice)
                {
                    var mailResponse =  await _mailService.SendEmailAsync(title, MailConstants.BuyMessage);
                    if (mailResponse !=null)
                    {
                        break;
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
