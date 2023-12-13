namespace Application.DTOs.Message
{
    using System.ComponentModel.DataAnnotations;
    
    using static Common.EntityValidationConstants.Message;
    using static Common.ExceptionMessages.Message;

    public class SaveMessageDto
    {
        [Required]
        public string MatchId { get; set; } = null!;

        [Required]
        public string AnimalId { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLength, ErrorMessage = InvalidContentLength)]
        public string Content { get; set; } = null!;
    }
}
