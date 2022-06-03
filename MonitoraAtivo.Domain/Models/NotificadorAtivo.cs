using MonitoraAtivo.Domain.Interfaces;
using MonitoraAtivo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Models
{
    public class NotificadorAtivo : IObserver
    {
        private readonly IMailService _mailService;
        private readonly ApplicationArgs _args;
        public NotificadorAtivo(IMailService mailService, ApplicationArgs args)
        {
            _mailService = mailService;
            _args = args;
        }
        public async void VerifyIfPriceHits(ISubject subject,Quote lastQuote)
        {
            var symbol = _args.Symbol;
            var sellPrice = _args.SellPrice;
            var buyPrice = _args.BuyPrice;
            var title = "Monitoração Ativo - " + symbol;
            var monitorador = subject as MonitoradorAtivo;
            string mailResponse;
            if (monitorador != null)
            {
                if ((monitorador.actualQuote.close>= sellPrice) && (lastQuote == null || lastQuote.close < sellPrice))
                {

                    mailResponse = await _mailService.SendEmailAsync(title, MailConstants.SellMessage);

                }
                else if (monitorador.actualQuote.close <= buyPrice && (lastQuote == null || lastQuote.close > buyPrice))
                {
                    mailResponse = await _mailService.SendEmailAsync(title, MailConstants.BuyMessage);
                }
            }
        }
    }
}

