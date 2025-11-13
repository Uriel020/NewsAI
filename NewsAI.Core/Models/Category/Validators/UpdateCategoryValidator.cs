using FluentValidation;

namespace NewsAI.Core.Models.Category.Validators
{
    public class UpdateCategoryValidator: AbstractValidator<UpdateCategoryDto>
    {
            public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
            .MaximumLength(20).WithMessage("Category name must not exceed 20 characters")
            .When(x => x.Name != null);

            RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Category description must not exceed 200 characters")
            .When(x => x.Description != null);
        }
    }
}