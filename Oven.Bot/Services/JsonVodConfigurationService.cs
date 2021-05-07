using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Oven.Bot.Models;
using Remora.Discord.API.Abstractions.Objects;

namespace Oven.Bot.Services
{
    public class JsonVodConfigurationService : IVodConfigurationService
    {
        private readonly IHttpClientFactory _factory;

        public JsonVodConfigurationService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        
        public async Task<(bool IsSuccess, string? ErrorMessage, VodConfigurationModel? VodConfiguration)> TryParseVodJsonConfigurationAsync(IAttachment? first)
        {
            Stream? response = null;

            if (first is null)
            {
                return (false, "FileNotFound", null);
            }
            
            try
            {
                response = await _factory.CreateClient().GetStreamAsync(first.Url);
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

        public void Save(VodConfigurationModel vodConfiguration)
        {
            
        }
    }
}