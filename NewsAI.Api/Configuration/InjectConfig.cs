using FluentValidation;
using NewsAI.Core.Mappers;
using NewsAI.Core.Models.Category;
using NewsAI.Core.Models.Category.Validators;
using NewsAI.Core.Models.News;
using NewsAI.Core.Models.News.Validators;
using NewsAI.Data.Repositories;
using NewsAI.Data.Services;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Api.Configuration;

public static class InjectConfig
{
    public static IServiceCollection ConfigureInjections( this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        //Services
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        //Validators
        services.AddScoped<IValidator<CreateNewsDto>, CreateNewsValidator>();
        services.AddScoped<IValidator<UpdateNewsDto>, UpdateNewsValidator>();
        services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
        services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();

        //Mappers
        services.AddAutoMapper(typeof(NewsProfile));

        return services;
    }
}