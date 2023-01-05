using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Models;

namespace FreeBilling.Services;

public class FakeEmailService : IEmailService
{
  public Task<bool> SendEmail(ContactModel model)
  {
    return Task.FromResult(true);
  }
}
