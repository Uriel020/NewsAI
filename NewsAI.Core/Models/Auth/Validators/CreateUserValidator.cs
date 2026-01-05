using FluentValidation;
using NewsAI.Core.Models.Auth.DTOs;

namespace NewsAI.Core.Models.Auth.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(20).WithMessage("First name must not exceed 20 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(20).WithMessage("Last name must not exceed 20 characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(12).WithMessage("Password must be at least 12 characters long");

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");
    }
}
