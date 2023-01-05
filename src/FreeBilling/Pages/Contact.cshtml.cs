using FreeBilling.Models;
using FreeBilling.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Pages;

public class ContactPageModel : PageModel
{
  private readonly IEmailService _emailService;

  public ContactPageModel(IEmailService emailService)
  {
    _emailService = emailService;
  }

  [BindProperty]
  public ContactModel Contact { get; set; } = new ContactModel();

  [BindProperty]
  public string? Message { get; set; }

  public void OnGet()
  {
  }

  public async void OnPost()
  {
    if (ModelState.IsValid && await _emailService.SendEmail(Contact))
    {
      Message = $"Message Sent";
      ModelState.Clear();
      Contact = new ContactModel();
    }
    else
    {
      Message = "Failed to send message";
    }
  }
}
