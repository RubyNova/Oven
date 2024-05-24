using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Oven.Data.Models;

namespace Oven.Data;

public static class DatabaseConfig
{
    public static IServiceCollection AddOvenDbContext(this IServiceCollection serviceCollection)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder("");
        dataSourceBuilder.MapEnum<AnswerKind>();
        var dataSource = dataSourceBuilder.Build();

        serviceCollection.AddDbContext<VodConfigurationContext>(options => options.UseNpgsql(dataSource));
        
        return serviceCollection;
    }
}