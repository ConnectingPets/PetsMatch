namespace Application.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        public string? PhotoUrl { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Token { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Education {  get; set; }
        
        public string? Gender { get; set; }

        public string? JobTitle { get; set; }
    }
}
