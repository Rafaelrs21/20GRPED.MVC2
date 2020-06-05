using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public class AutorHttpService : IAutorHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<BibliotecaHttpOptions> _bibliotecaHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AutorHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<BibliotecaHttpOptions> bibliotecaHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _bibliotecaHttpOptions = bibliotecaHttpOptions ?? throw new ArgumentNullException(nameof(bibliotecaHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;

            _httpClient = httpClientFactory?.CreateClient(bibliotecaHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.Timeout = TimeSpan.FromMinutes(_bibliotecaHttpOptions.CurrentValue.Timeout);
        }

        private async Task<bool> AddAuthJwtToRequest()
        {
            var jwtCookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("bibliotecaToken", out var jwtFromCookie);
            if (!jwtCookieExists)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtFromCookie);
            return true;
        }

        public async Task<IEnumerable<AutorEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }

            try
            {
                //exemplo recomendado com a nova API: System.Net.Http.Json
                var autores = await _httpClient.GetFromJsonAsync<List<AutorEntity>>(_bibliotecaHttpOptions.CurrentValue.AutorPath);

                return autores;
            }
            catch (HttpRequestException e) when (e.Message.Contains("401"))
            {
                await _signInManager.SignOutAsync();
                return null;
            }
        }

        public async Task<AutorEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.AutorPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                //await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<AutorEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetByIdHttpAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.AutorPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            return httpResponseMessage;
        }

        public async Task InsertAsync(AutorEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_bibliotecaHttpOptions.CurrentValue.AutorPath}";

            //exemplo recomendado com a nova API: System.Net.Http.Json
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(uriPath, insertedEntity);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(AutorEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.AutorPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.AutorPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
