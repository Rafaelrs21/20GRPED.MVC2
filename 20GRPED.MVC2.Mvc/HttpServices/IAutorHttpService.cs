using System.Net.Http;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public interface IAutorHttpService : IAutorService
    {
        //Exemplo passando tratamento de erro para a camada de Controller
        Task<HttpResponseMessage> GetByIdHttpAsync(int id);
    }
}
