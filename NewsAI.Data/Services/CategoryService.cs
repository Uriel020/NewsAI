using AutoMapper;
using FluentValidation;
using NewsAI.Core.Common;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.Category;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        private readonly IMapper _categoryMapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IValidator<CreateCategoryDto> createCategoryValidator,
            IValidator<UpdateCategoryDto> updateCategoryValidator,
            IMapper categoryMapper
            )
        {
            _categoryRepository = categoryRepository;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
            _categoryMapper = categoryMapper;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> FindAll()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var mapCategories = _categoryMapper.Map<IEnumerable<CategoryDto>>(categories);

            return Result<IEnumerable<CategoryDto>>.Success(mapCategories);
        }

        public async Task<Result<CategoryDto?>> FindById(Guid id)
        {
            var exist = await ValidateExist(id);

            if(!exist.IsSuccess) return Result<CategoryDto>.NotFound(exist.Error!)!;

            var mapCategory = _categoryMapper.Map<CategoryDto>(exist.Value);

            return Result<CategoryDto?>.Success(mapCategory);
        }

        public async Task<Result<Guid>> Create(CreateCategoryDto category)
        {
            var nameExist = await _categoryRepository.FindName(category.Name);

            if (nameExist) return Result<Guid>.Conflict("Name already exist");

            var validateCategory = _createCategoryValidator.Validate(category);

            if (!validateCategory.IsValid) return Result<Guid>.BadRequest(validateCategory.Errors);

            var mapCategory = _categoryMapper.Map<Category>(category);

            Category newCategory = await _categoryRepository.AddAsync(mapCategory);

            return Result<Guid>.Success(newCategory.Id);
        }

        public async Task<Result<bool>> Update(Guid id, UpdateCategoryDto category)
        {
            
            var exist = await ValidateExist(id);

            if(!exist.IsSuccess) return Result<bool>.NotFound(exist.Error!);

            if (category.Name != null)
            {
                var nameExist = await _categoryRepository.FindName(category.Name);

                if (nameExist) return Result<bool>.Conflict("Name already exist");
            }

            var validateCategory = _updateCategoryValidator.Validate(category);

            if (!validateCategory.IsValid) return Result<bool>.BadRequest(validateCategory.Errors);

            var oldCategory = (await _categoryRepository.GetByIdAsync(id))!;

            _categoryMapper.Map(category, oldCategory);

            await _categoryRepository.UpdateAsync(oldCategory);

            return Result<bool>.Success(true);

        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var exist = await ValidateExist(id);

            if(!exist.IsSuccess) return Result<bool>.NotFound(exist.Error!);

            await _categoryRepository.DeleteAsync(exist.Value.Id);

            return Result<bool>.Success(true);
        }

        public async Task<Result<Category>> ValidateExist(Guid id)
        {
            var exist = await _categoryRepository.GetByIdAsync(id);

            if (exist == null) return Result<Category>.NotFound("Category not found");

            return Result<Category>.Success(exist);
        }
    }
}