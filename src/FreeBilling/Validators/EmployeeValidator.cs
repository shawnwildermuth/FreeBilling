using FluentValidation;
using FreeBilling.Data.Entities;

namespace FreeBilling.Validators;

public class EmployeeValidator : AbstractValidator<Employee> 
{
  public EmployeeValidator()
  {
    RuleFor(e => e.Name)
      .NotEmpty()
      .MaximumLength(255);
      
    RuleFor(e => e.BillingRate)
      .NotEmpty();

  }
}