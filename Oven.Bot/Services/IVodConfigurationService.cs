using System.Linq;
using System.Threading.Tasks;
using Oven.Data.Models;
using Remora.Discord.API.Abstractions.Objects;

namespace Oven.Bot.Services
{
    public interface IVodConfigurationService
    {
        Task<(bool IsSuccess, string? ErrorMessage, VodConfigurationModel? VodConfiguration)>
            TryParseVodJsonConfigurationAsync(IAttachment? first);

        public Task<VodConfigurationModel> Get(string gameName);
        
        Task Save(VodConfigurationModel vodConfiguration);

        Task<int> Count();

        IQueryable<VodConfigurationModel> GetAll();
    }
}