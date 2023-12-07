namespace Application.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;

    using Application.Response;
    using Application.DTOs.Message;
    using Application.Service.Interfaces;

    using static Application.Message.SaveMessage;
    using static Application.Message.ChatHistory;

    public class MessageService : IMessageService
    {
        private readonly IMediator mediator;

        public MessageService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result<IEnumerable<ChatMessageDto>>> GetChatHistory(string matchId, string userId)
            => await this.mediator.Send(new ChatHistoryQuery
            {
                MatchId = matchId,
                UserId = userId
            });

        public async Task<Result<Unit>> SaveMessage(string matchId, string animalId, string message, string userId)
            => await this.mediator.Send(new SaveMessageCommand
            {
                MatchId = matchId,
                AnimalId = animalId,
                Content = message,
                UserId = userId
            });
    }
}
