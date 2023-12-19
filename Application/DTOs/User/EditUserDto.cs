namespace Application.DTOs.User
{
    using Domain.Enum;

    public class EditUserDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Description { get; set; }

        public int? Age { get; set; }

        public string? JobTitle { get; set; }

        public Gender? Gender { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Education { get; set; }
    }
}
