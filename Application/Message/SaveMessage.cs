﻿namespace Application.Message
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;

    using static Common.ExceptionMessages.Match;
    using static Common.ExceptionMessages.Animal;
    using static Common.FailMessages.Message;

    public class SaveMessage
    {
        public class SaveMessageCommand : IRequest<Result<Unit>>
        {
            public string AnimalId { get; set; } = null!;

            public string MatchId { get; set; } = null!;

            public string Content { get; set; } = null!;
        }

        public class SaveMessageHandler : IRequestHandler<SaveMessageCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public SaveMessageHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId.ToString() == request.AnimalId) == false)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Match>(animal => animal.MatchId.ToString() == request.MatchId) == false)
                {
                    return Result<Unit>.Failure(MatchNotFound);
                }

                Message message = new Message
                {
                    AnimalId = Guid.Parse(request.AnimalId),
                    MatchId = Guid.Parse(request.MatchId),
                    Content = request.Content,
                    SentOn = DateTime.Now
                };

                await this.repository.AddAsync(message);

                try
                {
                    await this.repository.SaveChangesAsync();
                    // What data to return
                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedSendMessage);
                }
            }
        }
    }
}
