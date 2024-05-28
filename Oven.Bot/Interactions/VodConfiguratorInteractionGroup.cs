using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Remora.Discord.Commands.Contexts;
using Remora.Discord.Interactivity;
using Remora.Results;

namespace Oven.Bot.Interactions;

public class VodConfiguratorInteractionGroup : InteractionGroup
{
    /*
    private readonly InteractionContext _context;

    public VodConfiguratorInteractionGroup(InteractionContext context)
    {
        _context = context;
    }


    [SelectMenu("test-menu")]
    public async Task<Result> OnMenuSelectionAsync(IReadOnlyList<string> values)
    {
        // `values` will contain the IDs of the selected users/roles

        if (!_context.Interaction.Data.TryGet(out var value) || value.AsT1 is null)
        {
            Console.WriteLine("REEEEE");
            return Result.FromSuccess(); // TODO: Make this a real error
        }

        var components = value.AsT1;
            // error, expected message component data to be present

        if (!components.Resolved.IsDefined(out var resolvedData))
        {
            Console.WriteLine("REEEEEEE 2");
            return Result.FromSuccess(); // todo: make this a real error
        }

        resolvedData.Users.IsDefined(out var userData);
        resolvedData.Members.IsDefined(out var memberData);
        resolvedData.Roles.IsDefined(out var roleData);        
    }
    */
}