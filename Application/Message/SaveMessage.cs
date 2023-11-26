namespace Application.Message
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Exceptions;

    public class SaveMessage
    {
        public class SaveMessageCommand : IRequest<Unit>
        {
            public string AnimalId { get; set; } = null!;

            public string MatchId { get; set; } = null!;

            public string Content { get; set; } = null!;
        }

        public class SaveMessageHandler : IRequestHandler<SaveMessageCommand, Unit>
        {
            private readonly IRepository repository;

            public SaveMessageHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.AnimalId, out Guid guidAnimalId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (!Guid.TryParse(request.MatchId, out Guid guidMatchId))
                {
                    throw new MatchNotFoundException();
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == guidAnimalId) == false)
                {
                    throw new AnimalNotFoundException();
                }

                if (await this.repository.AnyAsync<Match>(animal => animal.MatchId == guidMatchId) == false)
                {
                    throw new MatchNotFoundException();
                }

                Message message = new Message
                {
                    AnimalId = guidAnimalId,
                    MatchId = guidMatchId,
                    Content = request.Content,
                    SentOn = DateTime.Now
                };

                await this.repository.AddAsync(message);
                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
