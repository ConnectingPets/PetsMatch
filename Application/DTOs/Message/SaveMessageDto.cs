namespace Application.DTOs.Message
{
    using System.ComponentModel.DataAnnotations;

    public class SaveMessageDto
    {
        [Required]
        public string MatchId { get; set; } = null!;

        [Required]
        public string AnimalId { get; set; } = null!;

        [Required]
        []
        public string Content { get; set; } = null!;
    }
}
