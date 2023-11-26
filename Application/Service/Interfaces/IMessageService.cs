namespace Application.Service.Interfaces
{
    using Application.DTOs.Message;

    public interface IMessageService
    {
        Task SaveMessage(string matchId, string animalId, string message);

        Task<IEnumerable<ChatMessageDto>> GetChatHistory(string matchId);
    }
}
