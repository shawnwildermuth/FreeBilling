using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class AddressValidator : AbstractValidator<Address?>
{
  public AddressValidator()
  {
    RuleFor(e => e!.AddressLine1)
      .NotEmpty()
      .MaximumLength(255);

    RuleFor(e => e!.AddressLine2)
      .MaximumLength(255);

    RuleFor(e => e!.AddressLine3)
      .MaximumLength(255);

    RuleFor(e => e!.City)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(255);

    RuleFor(e => e!.StateProvince)
      .NotEmpty()
      .MaximumLength(25);

    RuleFor(e => e!.PostalCode)
      .NotEmpty()
      .MaximumLength(15);
  }
}