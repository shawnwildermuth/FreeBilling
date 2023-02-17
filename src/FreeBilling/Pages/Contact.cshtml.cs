using FreeBilling.Models;
using FreeBilling.Services;
using FreeBilling.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Pages;

public class ContactPageModel : PageModel
{
  private readonly IEmailService _emailService;
  private readonly ContactModelValidator _contactValidator;

  public ContactPageModel(IEmailService emailService, ContactModelValidator contactValidator)
  {
    _emailService = emailService;
    _contactValidator = contactValidator;
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
    if (_contactValidator.Validate(Contact).IsValid && await _emailService.SendEmail(Contact))
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
