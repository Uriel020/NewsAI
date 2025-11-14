using System.Data;
using FluentValidation;

namespace NewsAI.Core.Models.News.Validators;

public class CreateNewsValidator:AbstractValidator<CreateNewsDto>
{
    public CreateNewsValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("News title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("News description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
        
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("Url must be valid");
        
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .Must(id => id != Guid.Empty).WithMessage("CategoryId must be valid Guid");
    }
}