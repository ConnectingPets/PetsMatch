
namespace Domain.ViewModels
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Common.EntityValidationConstants.User;
    using static Common.ExceptionMessages.User;
    using Domain.Enum;
    using Microsoft.AspNetCore.Identity;

    public class RegisterUserDto
    {

        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength,MinimumLength = NameMinLength)]
        public required string Name { get; set; }


        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Email { get; set; }
    }
}