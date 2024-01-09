namespace Application.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.User;
    using static Common.ExceptionMessages.User;

    public class RegisterUserDto
    {
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = InvalidPassword)]
        [MinLength(PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required]
        public string[] Roles { get; set; } = null!;
    }
}