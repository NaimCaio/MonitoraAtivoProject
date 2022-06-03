using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoraAtivo.Domain.Interfaces
{
    public interface IMailService
    {
        Task<string> SendEmailAsync(string title, string content);
    }
}
