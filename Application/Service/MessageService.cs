namespace Application.Service
{
    using System.Threading.Tasks;

    using MediatR;

    using Application.Service.Interfaces;
    using static Application.Message.SaveMessage;

    public class MessageService : IMessageService
    {
        private readonly IMediator mediator;

        public MessageService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SaveMessage(string matchId, string animalId, string message)
            => await this.mediator.Send(new SaveMessageCommand
            {
                MatchId = matchId,
                AnimalId = animalId,
                Content = message
            });
    }
}
