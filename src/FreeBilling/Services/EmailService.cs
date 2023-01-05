using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Models;

namespace FreeBilling.Services;

public class EmailService : IEmailService
{
  public Task<bool> SendEmail(ContactModel model)
  {
    // Not implemented
    return Task.FromResult(false);
  }
}
