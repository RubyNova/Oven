using System.Threading.Tasks;
using Oven.Bot.Models;
using Remora.Discord.API.Abstractions.Objects;

namespace Oven.Bot.Services
{
    public interface IVodConfigurationService
    {
        Task<(bool IsSuccess, string? ErrorMessage, VodConfigurationModel? VodConfiguration)>
            TryParseVodJsonConfigurationAsync(IAttachment? first);
        void Save(VodConfigurationModel vodConfiguration);
    }
}