using System.Threading.Tasks;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Commands.Contexts;
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
        public Task<Result<IMessage>> PingAsync() => _channelApi.CreateMessageAsync(_context.ChannelID, "Pong!");
    }
}