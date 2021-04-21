using System.ComponentModel.DataAnnotations;

namespace LocalLibrary.API.Endpoints.Users.DTOs
{
    public class NewUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string[] Roles { get; set; }
    }
}
