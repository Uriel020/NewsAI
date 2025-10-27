using FluentValidation;
using NewsAI.Core.Mappers;
using NewsAI.Core.Models.News;
using NewsAI.Core.Models.News.Validators;
using NewsAI.Data.Repositories;
using NewsAI.Data.Service;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Api.Configuration;

public static class InjectConfig
{
    public static IServiceCollection ConfigureInjections( this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<INewsRepository, NewsRepository>();

        //Services
        services.AddScoped<INewsService, NewsService>();
        
        //Validators
        services.AddScoped<IValidator<CreateNewsDTO>, CreateNewsValidator>();
        services.AddScoped<IValidator<UpdateNewsDTO>, UpdateNewsValidator>();

        //Mappers
        services.AddAutoMapper(typeof(NewsProfile));

        
        return services;
    }
}