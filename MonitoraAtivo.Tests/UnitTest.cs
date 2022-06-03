using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitoraAtivo.Domain.BaseServices;
using MonitoraAtivo.Domain.Interfaces;
using MonitoraAtivo.Domain.Models;
using MonitoraAtivo.Domain.Models.Configuration;
using MonitoraAtivo.Domain.Shared;
using MonitoraAtivo.Services;
using MonitoraAtivo.Tests.Builders;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoraAtivo.Tests
{
    [TestClass]
    public class UnitTests : BaseTests
    {
        private ApplicationConfiguration _config;
        private ApplicationArgs _args;
        private readonly Mock<IMailService> _mailService;
        private readonly Mock<IObserver> _observer;
        public UnitTests() : base()
        {
            _mailService = new Mock<IMailService>();
        }

        [TestInitialize]
        public void Setup()
        {
            _config = new ApplicationConfiguration();
            _args = new ApplicationArgs("PETR4", 100,20 );
            base.SetupTestDependencies(_config, _args);
        }

        


        [TestMethod]
        public void VenderQuandoPrecoMaiorQueMaixmoInformado()
        {
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage)).Returns(Task.FromResult("e-mail de venda"));
            var monitorador = new MonitoradorAtivo();
            var notificador = new NotificadorAtivo(_mailService.Object, _args);
            monitorador.Atach(notificador);
            var ativoBuilder = new AtivoBuilder();
            var ativo = ativoBuilder.WithDateTime(DateTime.Now)
                .WithValue(120)
                .Build();
           
            monitorador.actualQuote = ativo.values[0];
            _mailService.Verify(x => x.SendEmailAsync( It.IsAny<string>(), MailConstants.SellMessage), Times.Once);
        }

        [TestMethod]
        public void ComprarQuandoPrecoMenorQueMinimoInformado()
        {
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage)).Returns(Task.FromResult("e-mail de compra"));
            var monitorador = new MonitoradorAtivo();
            var notificador = new NotificadorAtivo(_mailService.Object, _args);
            monitorador.Atach(notificador);
            var ativoBuilder = new AtivoBuilder();
            var ativo = ativoBuilder.WithDateTime(DateTime.Now)
                .WithValue(10)
                .Build();
           
            monitorador.actualQuote = ativo.values[0];
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage), Times.Once);
        }

        [TestMethod]
        public void NaoEnviarEmailEmValorIntermediario()
        {
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult("e-mail"));
            var monitorador = new MonitoradorAtivo();
            var notificador = new NotificadorAtivo(_mailService.Object, _args);
            monitorador.Atach(notificador);
            var ativoBuilder = new AtivoBuilder();
            var ativo = ativoBuilder.WithDateTime(DateTime.Now)
                .WithValue(50)
                .Build();
            
            monitorador.actualQuote = ativo.values[0];
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));
        }

        [TestMethod]
        public void MonitoracaoTeste()
        {
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage)).Returns(Task.FromResult("e-mail de venda"));
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage)).Returns(Task.FromResult("e-mail de compra"));
            var monitorador = new MonitoradorAtivo();
            var notificador = new NotificadorAtivo(_mailService.Object, _args);
            monitorador.Atach(notificador);
            var cotacoes = new List<Ativo>()
            {
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(50)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(120)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(50)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(10)
                .Build()
            };
            
            cotacoes.ForEach(c =>
            {
                monitorador.actualQuote = c.values[0];
            });
            
            
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage), Times.Once);
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage), Times.Once);
        }

        
    }
}
