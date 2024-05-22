using System.Threading.Tasks;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Commands.Contexts;
using Remora.Discord.Commands.Extensions;
using Remora.Results;

namespace Oven.Bot.Groups
{
    public class TestGroup : CommandGroup
    {
        private readonly IDiscordRestChannelAPI _channelApi;
        private readonly ICommandContext _context;

        public TestGroup(IDiscordRestChannelAPI channelApi, ICommandContext context)
        {
            _channelApi = channelApi;
            _context = context;
        }

        [Command("ping")]
        public async Task<Result<IMessage>> PingAsync()
        {
            if (_context.TryGetChannelID(out var channel))
            {
                return await _channelApi.CreateMessageAsync(channel, "Pong!");
            }

            return new InvalidOperationError("Unable to obtain Channel ID for ping response.");
        }
    }
}