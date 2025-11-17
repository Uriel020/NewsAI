using AutoMapper;
using FluentValidation;
using NewsAI.Core.Common;
using NewsAI.Core.Models.Category;
using NewsAI.Core.Models.Category.Validators;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryValidator> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryValidator> _updateCategoryValidator;
        private readonly IMapper _categoryMapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IValidator<CreateCategoryValidator> createCategoryValidator,
            IValidator<UpdateCategoryValidator> updateCategoryValidator,
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
            await ValidateExist(id);

            var category = await _categoryRepository.GetByIdAsync(id);

            var mapCategory = _categoryMapper.Map<CategoryDto>(category);

            return Result<CategoryDto?>.Success(mapCategory);
        }

        public Task<Result<Guid>> Create(CreateCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(Guid id, UpdateCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            await ValidateExist(id);

            await _categoryRepository.DeleteAsync(id);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ValidateExist(Guid id)
        {
            var exist = await _categoryRepository.GetByIdAsync(id);

            if(exist == null) return Result<bool>.NotFound("Category not found");

            return Result<bool>.Success(true);
        }
    }
}