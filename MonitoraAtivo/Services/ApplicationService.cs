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
        private readonly MonitoradorAtivo _monitoradorAtivo;
        private readonly IObserver _notificadordorAtivo;
        private readonly IFinanceService _financeService;
        private readonly ApplicationConfiguration _config;
        private readonly ApplicationArgs _args;

        public ApplicationService(IFinanceService financeService, ApplicationConfiguration config, IMailService mailService, ApplicationArgs args, IObserver notificadordorAtivo)
        {
            _monitoradorAtivo = new MonitoradorAtivo();
            _notificadordorAtivo = notificadordorAtivo;
            _monitoradorAtivo.Atach(notificadordorAtivo);
            _args = args;
            _financeService = financeService;
            _config = config;
        }
        public async void StartApplication()
        {
            
            await Monitorate();
        }

        
        private async Task Monitorate()
        {
            var symbol = _args.Symbol;
            while (true)
            {
                Console.WriteLine("Obtendo cotacao ativo " + symbol);
                var response = await _financeService.GetStockData(symbol);
                var actualQuote = response.values[0];
                var actualQuoteValue = actualQuote.close;
                Console.WriteLine("Ativo: " + symbol);
                Console.WriteLine("Fechamento: " + actualQuoteValue);
                Console.WriteLine("Horario: " + actualQuote.datetime);
                _monitoradorAtivo.actualQuote = actualQuote;
                Thread.Sleep(10000);
            }
        }
    }
}
