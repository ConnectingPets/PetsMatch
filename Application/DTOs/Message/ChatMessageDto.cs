namespace Application.DTOs.Message
{
    public class ChatMessageDto
    {
        public string Content { get; set; } = null!;

        public string SentOn { get; set; }

        public string AnimalId { get; set; } = null!;
    }
}
