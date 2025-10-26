using NewsAI.Core.Entities;
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
        services.AddScoped<ICommonService<News>, NewsService>();
        
        //Category
        
        return services;
    }
}