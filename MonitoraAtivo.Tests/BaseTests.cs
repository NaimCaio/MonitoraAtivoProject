using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitoraAtivo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Tests
{
    [TestClass]
    public abstract class BaseTests
    {
        protected readonly ServiceCollection _services = new ServiceCollection();

        public virtual void SetupTestDependencies(ApplicationConfiguration config, ApplicationArgs args)
        {
            _services.AddSingleton(config);
            _services.AddSingleton(args);

        }

    }
}
