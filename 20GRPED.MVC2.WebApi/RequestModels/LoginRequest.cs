using System.ComponentModel.DataAnnotations;

namespace _20GRPED.MVC2.WebApi.RequestModels
{
    public class LoginRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
