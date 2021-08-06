using BackEndExchange.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
  public interface IMailService
  {
    Task SendEmailAsync(MailRequest mailRequest);
  }
}
