using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Oven.Bot.Models;
using Oven.Bot.Services;

namespace Oven.Bot.Modules
{
    [Group("vodconfig")]
    public class VodConfiguratorModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVodConfigurationService _configurationService;

        public VodConfiguratorModule(IVodConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [Command("new")]
        public async Task AddNewVodConfigurationAsync()
        {
            if (Context.Message.Attachments.Any(x => x.Filename.Contains(".json")))
            {
                await ReplyAsync("It appears you have given me an existing JSON configuration! Validating...");

                var result = await _configurationService.TryParseVodJsonConfigurationAsync(
                    Context.Message.Attachments.First(x => x.Filename.Contains(".json")));

                if (!result.IsSuccess)
                {
                    await ReplyAsync(
                        $"JSON configuration failed! Reason: {result.ErrorMessage}");
                    return;
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

                await ReplyAsync(sb.ToString());
                _configurationService.Save(vodConfiguration);
                return;
            }
            
            //TODO: Implement Q&A format

            await ReplyAsync(
                "It appears no configuration file has been provided. See `help vodconfig add` for configuration file instructions.");
        }
    }
}