namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Persistence.Repositories;

    public class DeleteAnimal
    {
        public class DeleteAnimalCommand : IRequest<Result<Unit>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class DeleteAnimalCommandHandler :
            IRequestHandler<DeleteAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public DeleteAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;
                Animal? animal =
                    await repository.GetById<Animal>(Guid.Parse(animalId));

                try
                {
                    await repository.
                        DeleteAsync<Animal>(Guid.Parse(animalId));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure("This pet does not exist! Please select existing one.");
                }
                if (animal!.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure("This pet does not belong to you!");
                }


                repository.
                    DeleteRange<Swipe>(s => s.SwiperAnimalId.ToString() == animalId
                    || s.SwipeeAnimal.ToString() == animalId);

                AnimalMatch[] animalMatches = await repository.
                    All<AnimalMatch>(am => am.AnimalId.ToString() == animalId)
                    .ToArrayAsync();
                Guid[] matchesIds = animalMatches.
                    Select(am => am.MatchId)
                    .ToArray();
                Message[] allMessages = await repository.
                    All<Message>(m => m.AnimalId.ToString() == animalId).
                    ToArrayAsync();
                Guid[] allAnimalConversationId = allMessages.
                    Select(m => m.ConversationId).
                    ToArray();

                repository.DeleteRange(allMessages);
                repository.DeleteRange(animalMatches);

                foreach (var conversationId in allAnimalConversationId)
                {
                    await repository.DeleteAsync<Conversation>(conversationId);
                }
                foreach (var animalMatchId in matchesIds)
                {
                    await repository.DeleteAsync<Match>(animalMatchId);
                }

                try
                {
                    await repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, $"You have successfully delete {animal!.Name} to your pet's list");
                }
                catch (Exception)
                {
                    return
                        Result<Unit>.Failure($"Failed to delete pet - {animal.Name}");
                }

            }
        }
    }
}
