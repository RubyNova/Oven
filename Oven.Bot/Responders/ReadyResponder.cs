

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Remora.Discord.API.Abstractions.Gateway.Events;
using Remora.Discord.Commands.Services;
using Remora.Discord.Gateway.Responders;
using Remora.Rest.Core;
using Remora.Results;

namespace Oven.Bot.Responders;

public class ReadyResponder : IResponder<IReady>
{
    private readonly SlashService _slashService;
    private readonly IConfiguration _configurationService;

    public ReadyResponder(SlashService slashService, IConfiguration configurationService)
    {
        _slashService = slashService;
        _configurationService = configurationService;
    }


    public async Task<Result> RespondAsync(IReady gatewayEvent, CancellationToken ct = default)
    {
        #if DEBUG
        var debugServerString = _configurationService.GetValue<string?>("OVEN_DEBUG_SERVER");
        Snowflake? debugServerSnowflake = null;

        if(debugServerString is null || Snowflake.TryParse(debugServerString, out debugServerSnowflake))
        { 
            await _slashService.UpdateSlashCommandsAsync(ct: ct);
        }
        else
        {
            await _slashService.UpdateSlashCommandsAsync(debugServerSnowflake, ct: ct);
        }

        #else

        await _slashService.UpdateSlashCommandsAsync(ct: ct);

        #endif
        return Result.FromSuccess();
    }
}