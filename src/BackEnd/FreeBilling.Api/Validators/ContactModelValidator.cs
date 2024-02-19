using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class ContactModelValidator : AbstractValidator<ContactModel> 
{
  public ContactModelValidator()
  {
    RuleFor(e => e.Name)
      .NotEmpty();

    RuleFor(e => e.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(e => e.Subject)
      .NotEmpty();

    RuleFor(e => e.Message)
      .NotEmpty();
  }
}