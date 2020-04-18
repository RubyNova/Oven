using System;
using System.Threading.Tasks;
using Oven.Bot;

namespace Oven.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tcs = new TaskCompletionSource<bool>();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tcs.SetResult(true);
            };

            var t = new OvenDiscordClient().LaunchAsync();

            await Task.WhenAll(t, tcs.Task);
        }
    }
}