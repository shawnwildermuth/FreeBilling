using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
  public CustomerValidator(AddressValidator addressValidator)
  {
    RuleFor(e => e.CompanyName)
      .NotEmpty()
      .MaximumLength(255);

    RuleFor(e => e.Address)
      .SetValidator(addressValidator);

  }
}