using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Validators;

public class SigninValidator : AbstractValidator<SignInModel> 
{
  public SigninValidator()
  {
    RuleFor(e => e.Username)
      .NotEmpty();
      
    RuleFor(e => e.Password)
      .NotEmpty();

  }
}