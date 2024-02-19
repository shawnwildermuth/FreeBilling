using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Models;

namespace FreeBilling.Services;

public interface IEmailService
{
  Task<bool> SendEmail(ContactModel model);
}
