using FluentValidation;

namespace NewsAI.Core.Models.Category.Validators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(20);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required")
                .MaximumLength(500);
        }
    }
}