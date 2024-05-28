using System;
using System.Diagnostics;
using System.Threading.Tasks;
using OneOf;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.API.Objects;
using Remora.Discord.Commands.Attributes;
using Remora.Discord.Commands.Contexts;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Interactivity;
using Remora.Rest.Core;
using Remora.Results;

namespace Oven.Bot.Groups
{
    public class TestGroup : CommandGroup
    {
        private readonly IDiscordRestChannelAPI _channelApi;
        private readonly IInteractionContext _context;
        private readonly IDiscordRestInteractionAPI _interactionApi;

        public TestGroup(IDiscordRestChannelAPI channelApi, IInteractionContext context, IDiscordRestInteractionAPI interactionApi)
        {
            _channelApi = channelApi;
            _context = context;
            _interactionApi = interactionApi;
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

        [Command("modal")]
        [SuppressInteractionResponse(true)]
        public async Task<Result> ModalAsync()
        {
            var createConfirmationMessageResult = await _interactionApi.CreateInteractionResponseAsync(
                _context.Interaction.ID,
                _context.Interaction.Token,
                new InteractionResponse(InteractionCallbackType.ChannelMessageWithSource,
                new Optional<OneOf<IInteractionMessageCallbackData, IInteractionAutocompleteCallbackData, IInteractionModalCallbackData>>(
                    new InteractionMessageCallbackData
                    (
                        false,
                        "hello-world-content",
                        Components:
                        new[] 
                        {
                            new ActionRowComponent
                            (
                                [
                                    new ButtonComponent
                                    (
                                        ButtonComponentStyle.Primary,
                                        "Hello world!", CustomID: "ayy-lmao"
                                    )
                                ]
                            )
                        }
                    )
                )));

            if (!createConfirmationMessageResult.IsSuccess)
            {
                Console.WriteLine("FUCK"); 
            }

            return createConfirmationMessageResult;
        }
    }
}