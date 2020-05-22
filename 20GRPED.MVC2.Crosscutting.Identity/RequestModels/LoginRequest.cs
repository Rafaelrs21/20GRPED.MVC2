using System.ComponentModel.DataAnnotations;

namespace _20GRPED.MVC2.Crosscutting.Identity.RequestModels 
{ 
    public class LoginRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
