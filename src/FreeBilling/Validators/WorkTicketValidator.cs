using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class WorkTicketValidator : AbstractValidator<WorkTicket>
{
  public WorkTicketValidator()
  {
    RuleFor(e => e.Hours)
      .InclusiveBetween(.1f, 12f);

    RuleFor(e => e.BillingRate)
      .InclusiveBetween(10f, 1000f);

    RuleFor(e => e.Date)
      .NotEmpty();

    RuleFor(e => e.Project)
      .NotNull();

    RuleFor(e => e.WorkPerformed)
      .NotEmpty()
      .MaximumLength(5000);

  }
}