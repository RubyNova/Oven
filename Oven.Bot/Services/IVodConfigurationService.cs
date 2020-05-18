using System.Threading.Tasks;
using Discord;
using Oven.Bot.Models;

namespace Oven.Bot.Services
{
    public interface IVodConfigurationService
    {
        Task<(bool IsSuccess, string? ErrorMessage, VodConfigurationModel? VodConfiguration)> TryParseVodJsonConfigurationAsync(Attachment first);
        void Save(VodConfigurationModel vodConfiguration);
    }
}