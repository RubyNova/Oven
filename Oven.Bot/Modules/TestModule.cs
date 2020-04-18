using System.Threading.Tasks;
using Discord.Commands;

namespace Oven.Bot.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public Task PingAsync() => Context.Channel.SendMessageAsync("pong!");
    }
}