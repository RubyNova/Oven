using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Oven.Data;
using Oven.Data.Models;
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

        public async Task<(bool IsSuccess, string? ErrorMessage, VodConfiguration? VodConfiguration)> TryParseVodJsonConfigurationAsync(IAttachment? first)
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
                var modelResult = await JsonSerializer.DeserializeAsync<VodConfiguration>(response);
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

        public async Task SaveAsync(VodConfiguration vodConfiguration)
        {
            using var db = new QuestionaireContext();

            var result = await db.VodConfigurations.FindAsync(vodConfiguration.GameName);
            if (result is not null)
            {
                vodConfiguration.VodConfigurationId = result.VodConfigurationId;
                db.Update(vodConfiguration);
            }

            db.Add(vodConfiguration);
            await db.SaveChangesAsync();
        }
    }
}