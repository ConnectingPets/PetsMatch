﻿namespace Application.DTOs.User
{
    public class UserProfileDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; } = null!;

        public string? Address { get; set; } = null!;

        public string? City { get; set; } = null!;

        public string? Education { get; set; } = null!;

        public string? JobTitle { get; set; } = null!;

        public string? Photo {  get; set; } = null!;
    }
}
