namespace Application.Service.Interfaces
{
    using Application.DTOs.Message;
    using Application.Response;
    using MediatR;

    public interface IMessageService
    {
        Task<Result<Unit>> SaveMessage(string matchId, string animalId, string message, string userId);

        Task<Result<IEnumerable<ChatMessageDto>>> GetChatHistory(string matchId, string userId);
    }
}
