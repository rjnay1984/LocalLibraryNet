using System.ComponentModel.DataAnnotations;

namespace LocalLibrary.API.Endpoints.Account.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}