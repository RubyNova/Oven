using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oven.Bot.Errors;
using Oven.Bot.Models;
using Oven.Bot.Services;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Commands.Contexts;
using Remora.Results;

namespace Oven.Bot.Groups
{
    [Group("vodconfig")]
    public class VodConfiguratorGroup : CommandGroup
    {
        private readonly IDiscordRestChannelAPI _channelApi;
        private readonly MessageContext _context;
        private readonly IVodConfigurationService _configurationService;

        public VodConfiguratorGroup(IDiscordRestChannelAPI channelApi, MessageContext context,
            IVodConfigurationService configurationService)
        {
            _channelApi = channelApi;
            _context = context;
            _configurationService = configurationService;
        }

        [Command("new")]
        public async Task<Result> AddNewVodConfigurationAsync()
        {
            if (_context.Message.Attachments.HasValue)
            {
                await _channelApi.CreateMessageAsync(_context.ChannelID,
                    "It appears you may have given me an existing JSON configuration! Validating...");

                var result = await _configurationService.TryParseVodJsonConfigurationAsync(
                    _context.Message.Attachments.Value.FirstOrDefault(x => x.Filename.Contains(".json")));

                if (!result.IsSuccess)
                {
                    await _channelApi.CreateMessageAsync(_context.ChannelID,
                        $"JSON configuration failed! Reason: {result.ErrorMessage}");
                    return new UserError(result.ErrorMessage ?? "Malformed or Nonexistent JSON. No specific error returned.");
                }

                var vodConfiguration = result.VodConfiguration;

                var sb = new StringBuilder();
                sb.AppendLine("**Configuration successfully validated!**");
                sb.AppendLine("**This is what I have understood from your configuration file:**");
                sb.AppendLine();
                sb.AppendLine("**Game Name:**");
                sb.AppendLine(vodConfiguration!.GameName);
                sb.AppendLine();

                for (var i = 0; i < vodConfiguration.Questions.Count; i++)
                {
                    var question = vodConfiguration.Questions[i];
                    sb.Append($"**Question ");
                    sb.Append(i);
                    sb.AppendLine(":**");
                    sb.AppendLine(question.QuestionText);
                    sb.AppendLine("**Answer Kind:**");
                    sb.AppendLine(question.UserAnswerKind.ToString());

                    switch (question.UserAnswerKind)
                    {
                        case AnswerKind.Text:
                            break;
                        case AnswerKind.Integer:
                            break;
                        case AnswerKind.Decimal:
                            break;
                        case AnswerKind.CustomScale:
                            sb.AppendLine("**Scale Values:**");

                            sb.Append(question.MinimumScale!.Value);
                            sb.Append(" Minimum, ");
                            sb.Append(question.MaximumScale!.Value);
                            sb.AppendLine(" Maximum.");
                            break;
                        case AnswerKind.Percentage:
                            break;
                        case AnswerKind.MultipleChoice:
                            sb.AppendLine("**Answer Options:**");
                            foreach (var answerOption in question.AnswerOptions!)
                            {
                                sb.Append(answerOption.Id);
                                sb.Append(" - ");
                                sb.Append(answerOption.OptionTitle);

                                if (answerOption.ShouldRejectIfChosen)
                                {
                                    sb.Append(" - ATTENTION: This option will cause an auto rejection!");
                                }

                                sb.AppendLine();
                            }

                            break;
                        case AnswerKind.Url:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    sb.AppendLine();
                }

                await _channelApi.CreateMessageAsync(_context.ChannelID, sb.ToString());
                _configurationService.Save(vodConfiguration);
                return Result.FromSuccess();
            }

            //TODO: Implement Q&A format

            await _channelApi.CreateMessageAsync(_context.ChannelID,
                "It appears no configuration file has been provided. See `help vodconfig add` for configuration file instructions.");
            return new UserError("No vodconfig provided.");
        }
    }
}