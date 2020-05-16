using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using System.Text.Json;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public class LivroHttpService : ILivroService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<BibliotecaHttpOptions> _bibliotecaHttpOptions;

        public LivroHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<BibliotecaHttpOptions> bibliotecaHttpOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _bibliotecaHttpOptions = bibliotecaHttpOptions ?? throw new ArgumentNullException(nameof(bibliotecaHttpOptions));

            _httpClient = httpClientFactory.CreateClient(bibliotecaHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_bibliotecaHttpOptions.CurrentValue.Timeout);
        }

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync(_bibliotecaHttpOptions.CurrentValue.LivroPath);
            return JsonConvert.DeserializeObject<List<LivroEntity>>(result);
        }

        public async Task<LivroEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<LivroEntity>(result);
        }

        public async Task InsertAsync(LivroEntity insertedEntity)
        {
            var uriPath = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(uriPath, httpContent);
        }

        public async Task UpdateAsync(LivroEntity updatedEntity)
        {
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(pathWithId, httpContent);
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{id}";
            await _httpClient.DeleteAsync(pathWithId);
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id)
        {
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/CheckIsbn/{isbn}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return bool.Parse(result);
        }
    }
}
