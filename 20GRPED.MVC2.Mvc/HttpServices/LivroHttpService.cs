using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using System.Text.Json;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public class LivroHttpService : ILivroService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<BibliotecaHttpOptions> _bibliotecaHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LivroHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<BibliotecaHttpOptions> bibliotecaHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _bibliotecaHttpOptions = bibliotecaHttpOptions ?? throw new ArgumentNullException(nameof(bibliotecaHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory.CreateClient(bibliotecaHttpOptions.CurrentValue.Name);
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

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_bibliotecaHttpOptions.CurrentValue.LivroPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<LivroEntity>>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
            }

            return null;
        }

        public async Task<LivroEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LivroEntity>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
                new RedirectToActionResult("Livro", "Index", null);
            }

            return null;
        }

        public async Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(livroAutorAggregateEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(LivroEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{updatedEntity.Id}";

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
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return false; //rever, pode ser que tenha que inverter ou levantar uma exceção
            }

            await AddAuthJwtToRequest();
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.LivroPath}/CheckIsbn/{isbn}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            return bool.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}
