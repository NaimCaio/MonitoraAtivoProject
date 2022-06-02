using Microsoft.Extensions.Configuration;
using MonitoraAtivo.Model;
using MonitoraAtivo.Model.Interfaces;
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
        public async void startApplication()
        {
            //Parallel.Invoke(() => Monitorate(args), ()=>printar());
            var t = Task.Run(() => Monitorate());
            t.Wait();
        }

        
        private async void Monitorate()
        {
            Console.WriteLine("obtendo cotacao");
            var symbol = _args.Symbol;
            var sellPrice = decimal.Parse(_args.SellPrice);
            var buyPrice = decimal.Parse(_args.BuyPrice);
            while (true)
            {
                Console.WriteLine("obtendo cotacao");
                var response = await _financeService.getStockData(symbol);
                var lastQuote = response.values[0];
                var lastValue = lastQuote.close;
                if (decimal.Parse(lastQuote.close) >= sellPrice)
                {
                    var mailResponse= await _mailService.SendEmailAsync("comprar", "comprar");
                    if (mailResponse != null)
                    {
                        break;
                    }
                    
                }
                else if (decimal.Parse(lastQuote.close) <= buyPrice)
                {
                    var mailResponse =  await _mailService.SendEmailAsync("vendaa", "venda");
                    if (mailResponse !=null)
                    {
                        break;
                    }
                }
                Console.WriteLine(DateTime.Now);
                Thread.Sleep(10000);
                Console.WriteLine(DateTime.Now);
            }
        }
    }
}
