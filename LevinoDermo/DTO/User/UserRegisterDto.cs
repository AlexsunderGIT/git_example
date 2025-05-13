using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.DTO
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 character in length.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 50 character in length.")]
        public string Password { get; set; }
    }
}