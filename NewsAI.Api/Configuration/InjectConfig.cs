using FluentValidation;
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
        //News
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IValidator<CreateNewsDTO>, CreateNewsValidator>();
        services.AddScoped<IValidator<UpdateNewsDTO>, UpdateNewsValidator>();

        //Category
        
        return services;
    }
}