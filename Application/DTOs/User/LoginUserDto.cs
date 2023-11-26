namespace Application.DTOs.User
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ExceptionMessages.User;
    using static Common.EntityValidationConstants.User;

    public class LoginUserDto
    {
        [Required]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password, ErrorMessage = InvalidPassword)]
        [MinLength(PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
