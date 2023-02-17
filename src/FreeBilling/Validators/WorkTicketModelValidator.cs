using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class WorkTicketModelValidator : AbstractValidator<WorkTicketModel>
{
  public WorkTicketModelValidator()
  {
    RuleFor(e => e.Hours)
      .InclusiveBetween(.1f, 12f);

    RuleFor(e => e.BillingRate)
      .InclusiveBetween(10f, 1000f);

    RuleFor(e => e.Date)
      .NotEmpty();

    RuleFor(e => e.ProjectId)
      .NotEqual(0);

    RuleFor(e => e.WorkPerformed)
      .NotEmpty()
      .MaximumLength(5000);

  }
}