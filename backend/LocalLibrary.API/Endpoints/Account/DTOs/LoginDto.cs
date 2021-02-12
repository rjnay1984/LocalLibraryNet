using System.ComponentModel.DataAnnotations;

namespace LocalLibrary.API.Endpoints.Account.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
