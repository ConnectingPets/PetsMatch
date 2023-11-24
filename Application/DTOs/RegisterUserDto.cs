namespace Application.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.User;
    using static Common.ExceptionMessages.User;

    public class RegisterUserDto
    {
        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength, MinimumLength = NameMinLength)]
        public required string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}