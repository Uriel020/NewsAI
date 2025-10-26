using FluentValidation;

namespace NewsAI.Core.Models.News.Validators;

public class UpdateNewsValidator : AbstractValidator<UpdateNewsDTO>
{
    public UpdateNewsValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters")
            .When(x => x.Title != null);
            
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters")
            .When(x => x.Title != null);
        
        RuleFor(x => x.Views)
            .GreaterThan(0)
            .WithMessage("Views must be greater than 0")
            .When(x => x.Views.HasValue);

        RuleFor(x => x.HotNews)
            .NotNull()
            .WithMessage("HotNews must be provided")
            .When(x => x.HotNews.HasValue);
        
        RuleFor(x => x.CategoryId)
            .Must(id => id != Guid.Empty).WithMessage("CategoryId must be valid Guid");
    }
}