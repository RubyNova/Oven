using System.Threading.Tasks;
using Oven.Data.Models;
using Remora.Discord.API.Abstractions.Objects;

namespace Oven.Bot.Services
{
    public interface IVodConfigurationService
    {
        Task<(bool IsSuccess, string? ErrorMessage, VodConfiguration? VodConfiguration)>
            TryParseVodJsonConfigurationAsync(IAttachment? first);
        Task SaveAsync(VodConfiguration vodConfiguration);
    }
}