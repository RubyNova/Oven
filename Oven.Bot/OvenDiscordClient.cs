using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oven.Bot.Groups;
using Oven.Bot.Services;
using Remora.Commands.Extensions;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Commands.Services;
using Remora.Discord.Gateway;
using Microsoft.Extensions.Configuration;
using Remora.Discord.Hosting.Extensions;
using Remora.Discord.API.Abstractions.Gateway.Commands;
using Remora.Rest.Core;
using Remora.Discord.API;

namespace Oven.Bot;

public class OvenDiscordClient
{
    public async Task LaunchAsync()
    {
        var host = CreateHostBuilder([])
        .UseConsoleLifetime()
        .Build();

        var services = host.Services;
        var log = services.GetRequiredService<ILogger<OvenDiscordClient>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        Snowflake? debugServer = null;

#if DEBUG
        var debugServerString = configuration.GetValue<string?>("OVEN_DEBUG_SERVER");
        if (debugServerString is not null)
        {
            if (!DiscordSnowflake.TryParse(debugServerString, out debugServer))
            {
                log.LogWarning("Unable to locate the specified Oven debug server in the environment variables. Please set the Server ID via the OVEN_DEBUG_SERVER environment variable.");
            }
        }
#endif

        var slashService = services.GetRequiredService<SlashService>();
        var updateSlash = await slashService.UpdateSlashCommandsAsync(debugServer);
        if (!updateSlash.IsSuccess)
        {
            log.LogWarning("Failed to update slash commands: {Reason}", updateSlash.Error.Message);
        }

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
    .CreateDefaultBuilder(args)
    .AddDiscordService(services =>
    {
        return services.GetRequiredService<IConfiguration>().GetValue<string?>("OVEN_BOT_TOKEN") ??
        throw new InvalidOperationException("The Oven bot token is not present. Please set the token via the OVEN_BOT_TOKEN environment variable.");
    })
    .ConfigureServices((_, services) =>
    {
        services.Configure<DiscordGatewayClientOptions>(g => g.Intents |= GatewayIntents.MessageContents);

        services.AddDiscordCommands(true)
        .AddTransient<IVodConfigurationService, JsonVodConfigurationService>()
        .AddHttpClient()
        .AddCommandTree()
        .WithCommandGroup<TestGroup>()
        .WithCommandGroup<VodConfiguratorGroup>()
        .Finish();

    })
    .ConfigureLogging
    (
        c => c
            .AddConsole()
            .AddFilter("System.Net.Http.HttpClient.*.LogicalHandler", LogLevel.Warning)
            .AddFilter("System.Net.Http.HttpClient.*.ClientHandler", LogLevel.Warning)
    );
}
