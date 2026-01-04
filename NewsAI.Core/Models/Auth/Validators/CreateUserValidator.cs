using FluentValidation;
using NewsAI.Core.Models.Auth.DTOs;

namespace NewsAI.Core.Models.Auth.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(20).WithMessage("First name must be not exceed 20 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(20).WithMessage("Last name must be not exceed 20 characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}


//   public string FirstName { get; set; } = null!;

//     public string LastName { get; set; } = null!;

//     public string? EmailAddress { get; set; }

//     public string Password { get; set; } = null!;

//     public int CardNumber { get; set; }

//     public int PhoneNumber { get; set; }

//     public DateTime DateOfBirth { get; set; }