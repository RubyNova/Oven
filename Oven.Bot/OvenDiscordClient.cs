using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Oven.Bot.Services;

namespace Oven.Bot
{
    public class OvenDiscordClient
    {
        private DiscordSocketClient _client;


        public async Task LaunchAsync()
        {
            _client = new DiscordSocketClient();
            var services = ConfigureServices();
            await services.GetRequiredService<CommandHandlingService>().InitialiseAsync(services).ConfigureAwait(false);
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("BOT_TOKEN")).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<LoggingService>()
                .BuildServiceProvider();
        }
    }
}