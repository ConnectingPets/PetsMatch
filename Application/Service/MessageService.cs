namespace Application.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;

    using Application.Service.Interfaces;
    using Application.DTOs.Message;
    using static Application.Message.SaveMessage;
    using static Application.Message.ChatHistory;

    public class MessageService : IMessageService
    {
        private readonly IMediator mediator;

        public MessageService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<ChatMessageDto>> GetChatHistory(string matchId)
            => await this.mediator.Send(new ChatHistoryQuery
            {
                MatchId = matchId
            });

        public async Task SaveMessage(string matchId, string animalId, string message)
            => await this.mediator.Send(new SaveMessageCommand
            {
                MatchId = matchId,
                AnimalId = animalId,
                Content = message
            });
    }
}
