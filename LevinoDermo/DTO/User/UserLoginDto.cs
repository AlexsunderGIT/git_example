using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.DTO.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}