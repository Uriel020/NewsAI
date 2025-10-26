using Microsoft.EntityFrameworkCore;
using NewsAI.Data.Context;

namespace NewsAI.Api.Configuration;

public static class DBConfig
{
    public static IServiceCollection ConfigureDb (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NewsAIConnection");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<NewsDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }


}