using FluentValidation;
using NewsAI.Core.Mappers;
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
        
        //Validators
        services.AddScoped<IValidator<CreateNewsDto>, CreateNewsValidator>();
        services.AddScoped<IValidator<UpdateNewsDto>, UpdateNewsValidator>();

        //Mappers
        services.AddAutoMapper(typeof(NewsProfile));

        return services;
    }
}