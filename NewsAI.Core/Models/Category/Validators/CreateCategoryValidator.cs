using FluentValidation;

namespace NewsAI.Core.Models.Category.Validators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(20).WithMessage("Category name must not exceed 20 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required")
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters");
        }
    }
}