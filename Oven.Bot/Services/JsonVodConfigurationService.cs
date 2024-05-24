using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oven.Data;
using Oven.Data.Models;
using Remora.Discord.API.Abstractions.Objects;

namespace Oven.Bot.Services
{
    public class JsonVodConfigurationService(
        IHttpClientFactory factory,
        VodConfigurationContext vodConfigurationContext)
        : IVodConfigurationService
    {
        public async Task<(bool IsSuccess, string? ErrorMessage, VodConfigurationModel? VodConfiguration)> TryParseVodJsonConfigurationAsync(IAttachment? first)
        {
            Stream? response = null;

            if (first is null)
            {
                return (false, "FileNotFound", null);
            }
            
            try
            {
                response = await factory.CreateClient().GetStreamAsync(first.Url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                if (response is Stream) await response.DisposeAsync();
                return (false, e.Message, null);
            }

            try
            {
                var modelResult = await JsonSerializer.DeserializeAsync<VodConfigurationModel>(response);
                await response.DisposeAsync();
                return (true, null, modelResult);
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                await response.DisposeAsync();
                return (false, e.Message, null);
            }
        }

        public async Task<VodConfigurationModel> Get(string gameName)
        {
            return await vodConfigurationContext.VodConfigurations.FirstAsync(x => x.GameName == gameName);
        }

        public async Task Save(VodConfigurationModel vodConfiguration)
        {
            vodConfigurationContext.VodConfigurations.Update(vodConfiguration);
            await vodConfigurationContext.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await vodConfigurationContext.VodConfigurations.CountAsync();
        }

        public IQueryable<VodConfigurationModel> GetAll()
        {
            return vodConfigurationContext.VodConfigurations;
        }
    }
}