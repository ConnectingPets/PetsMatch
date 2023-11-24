namespace Application.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        public string? Photo { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Token {  get; set; } 
    }
}
