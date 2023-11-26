namespace Application.DTOs.Match
{
    using System.ComponentModel.DataAnnotations;

    public class MatchDto
    {
        [Required]
        public required string AnimalOneId { get; set; }

        [Required]
        public required string AnimalTwoId { get; set; }
    }
}
