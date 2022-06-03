using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private readonly Mock<IFinanceService> _financeService;
        public UnitTests() : base()
        {
            _mailService = new Mock<IMailService>();
            _financeService = new Mock<IFinanceService>();
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

            var applicationService = new ApplicationService(_financeService.Object, _config, _mailService.Object, _args);
            var ativoBuilder = new AtivoBuilder();
            var ativo = ativoBuilder.WithDateTime(DateTime.Now)
                .WithValue(120)
                .Build();
            _financeService.Setup(x => x.GetStockData(_args.Symbol)).Returns(Task.FromResult(ativo));
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage)).Returns(Task.FromResult("e-mail de venda"));
            applicationService.StartApplication();
            _mailService.Verify(x => x.SendEmailAsync( It.IsAny<string>(), MailConstants.SellMessage), Times.Once);
        }

        [TestMethod]
        public void ComprarQuandoPrecoMenorQueMinimoInformado()
        {

            var applicationService = new ApplicationService(_financeService.Object, _config, _mailService.Object, _args);
            var ativoBuilder = new AtivoBuilder();
            var ativo = ativoBuilder.WithDateTime(DateTime.Now)
                .WithValue(10)
                .Build();
            _financeService.Setup(x => x.GetStockData(_args.Symbol)).Returns(Task.FromResult(ativo));
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage)).Returns(Task.FromResult("e-mail de compra"));
            applicationService.StartApplication();
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage), Times.Once);
        }

        [TestMethod]
        public void MonitoracaoTesteVenda()
        {

            var applicationService = new ApplicationService(_financeService.Object, _config, _mailService.Object, _args);
             
            var cotacoes = new List<Ativo>()
            {
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(50)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(120)
                .Build()
            };
            var quantidaddeCotacoes = cotacoes.Count();
            _financeService.Setup(x => x.GetStockData(_args.Symbol)).Returns(() => {
                var ativoRetornado = Task.FromResult(cotacoes.FirstOrDefault());
                cotacoes.RemoveAt(0);
                return ativoRetornado;
                });
            
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage)).Returns(Task.FromResult("e-mail de compra"));
            applicationService.StartApplication();
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.SellMessage), Times.Once);
            _financeService.Verify(x => x.GetStockData(It.IsAny<string>()), Times.Exactly(quantidaddeCotacoes));
        }

        [TestMethod]
        public void MonitoracaoTesteCompra()
        {
            var applicationService = new ApplicationService(_financeService.Object, _config, _mailService.Object, _args);
            var cotacoes = new List<Ativo>()
            {
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(50)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(30)
                .Build(),
                new AtivoBuilder().WithDateTime(DateTime.Now)
                .WithValue(10)
                .Build()
            };
            var quantidaddeCotacoes = cotacoes.Count();
            _financeService.Setup(x => x.GetStockData(_args.Symbol)).Returns(() => {
                var ativoRetornado = Task.FromResult(cotacoes.FirstOrDefault());
                cotacoes.RemoveAt(0);
                return ativoRetornado;
            });
            _mailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage)).Returns(Task.FromResult("e-mail de compra"));
            applicationService.StartApplication();
            _mailService.Verify(x => x.SendEmailAsync(It.IsAny<string>(), MailConstants.BuyMessage), Times.Once);
            _financeService.Verify(x => x.GetStockData(It.IsAny<string>()), Times.Exactly(quantidaddeCotacoes));
        }
    }
}
