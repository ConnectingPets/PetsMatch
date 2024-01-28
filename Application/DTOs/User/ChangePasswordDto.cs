namespace Application.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ExceptionMessages.User;
    using static Common.EntityValidationConstants.User;

    public class ChangePasswordDto
    {
        [Required]
        [DataType(DataType.Password, ErrorMessage = InvalidPassword)]
        public string OldPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.Password, ErrorMessage = InvalidPassword)]
        [MinLength(PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        public string NewPassword { get; set; } = null!;

        [Compare(nameof(NewPassword), ErrorMessage = PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
