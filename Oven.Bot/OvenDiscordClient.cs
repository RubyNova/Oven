using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oven.Bot.Groups;
using Oven.Bot.Services;
using Remora.Commands.Extensions;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Gateway;
using Remora.Discord.Gateway.Extensions;

namespace Oven.Bot
{
    public class OvenDiscordClient
    {
        private IServiceProvider? _collection;
        
        public async Task LaunchAsync()
        {
            _collection = ConfigureServices();
            await _collection.GetRequiredService<DiscordGatewayClient>().RunAsync(CancellationToken.None);
        }

        private IServiceProvider ConfigureServices()
        {
            var token = Environment.GetEnvironmentVariable("BOT_TOKEN");
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return new ServiceCollection()
                .AddLogging
                (
                    x => x.AddConsole()
                        .AddFilter("System.Net.Http.HttpClient.*.LogicalHandler", LogLevel.Warning)
                        .AddFilter("System.Net.Http.HttpClient.*.ClientHandler", LogLevel.Warning)
                )
                .AddDiscordGateway(_ => token)
                .AddDiscordCommands()
                .AddCommandGroup<TestGroup>()
                .AddCommandGroup<VodConfiguratorGroup>()
                .AddHttpClient()
                .AddTransient<IVodConfigurationService, JsonVodConfigurationService>()
                .BuildServiceProvider();
        }
    }
}