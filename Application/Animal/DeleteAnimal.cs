namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;

    using static Common.SuccessMessages.Animal;
    using static Common.ExceptionMessages.Animal;

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
            private readonly IPhotoService photoService;

            public DeleteAnimalCommandHandler(IRepository repository,
                                              IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;
                Animal? animal =
                    await repository.FirstOrDefaultAsync<Animal>(a => a.AnimalId.ToString() == animalId);

                Photo[] animalPhotos =  repository.All<Photo>(p => p.AnimalId.ToString() == animalId).ToArray();

                foreach (Photo photo in animalPhotos)
                {
                    await photoService.DeleteAnimalPhotoAsync(photo);
                }

                try
                {
                    await repository.
                        DeleteAsync<Animal>(Guid.Parse(animalId));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }
                if (animal!.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure(NotRightUser);
                }

                repository.
                    DeleteRange<Swipe>(s => s.SwiperAnimalId.ToString() == animalId
                    || s.SwipeeAnimalId.ToString() == animalId);

                AnimalMatch[] animalMatch =  repository.
                    All<AnimalMatch>(am => am.AnimalId.ToString() == animalId).ToArray();

                foreach (AnimalMatch match in animalMatch)
                {
                    AnimalMatch[] animalMatches =  repository.All<AnimalMatch>(am => am.MatchId == match.MatchId).ToArray();

                    Guid[] matchesIds = animalMatches.
                        Select(am => am.MatchId)
                        .ToArray();

                    repository.DeleteRange(animalMatches);

                    foreach (var animalMatchId in matchesIds)
                    {
                        await repository.DeleteAsync<Match>(animalMatchId);
                    }
                }

                Message[] allMessages =  repository.
                    All<Message>(m => m.AnimalId.ToString() == animalId).
                    ToArray();

                repository.DeleteRange(allMessages);

                try
                {
                    await repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value,String.Format(SuccessfullyDeleteAnimal,animal.Name));
                }
                catch (Exception)
                {
                    return
                        Result<Unit>.Failure(string.Format(FailedToDelete, animal.Name));
                }
            }
        }
    }
}
