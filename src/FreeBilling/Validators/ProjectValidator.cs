using FluentValidation;
using FreeBilling.Data.Entities;

namespace FreeBilling.Validators;

public class ProjectValidator : AbstractValidator<Project> 
{
  public ProjectValidator()
  {
    RuleFor(e => e.ProjectName)
      .NotNull()
      .MaximumLength(255);
      
    RuleFor(e => e.Customer)
      .NotNull();

  }
}