using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } 
    }
}
