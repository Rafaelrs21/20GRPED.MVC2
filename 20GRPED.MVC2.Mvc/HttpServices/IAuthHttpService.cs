using System.Threading.Tasks;
using _20GRPED.MVC2.Crosscutting.Identity.RequestModels;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public interface IAuthHttpService
    {
        Task<string> GetTokenAsync(LoginRequest loginRequest);
    }
}