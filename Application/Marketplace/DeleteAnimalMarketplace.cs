namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Animal;
    using static Common.SuccessMessages.Animal;

    public class DeleteAnimalMarketplace
    {
        public class DeleteAnimalMarketplaceCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;

            public string AnimalId { get; set; } = null!;
        }

        public class DeleteAnimalMarketplaceCommandHandler : IRequestHandler<DeleteAnimalMarketplaceCommand, Result<Unit>>
        {
            private readonly IRepository repository;
            private readonly IPhotoService photoService;

            public DeleteAnimalMarketplaceCommandHandler(
                IRepository repository,
                IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(DeleteAnimalMarketplaceCommand request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;
                Animal? animal =
                    await repository.FirstOrDefaultAsync<Animal>(a => a.AnimalId.ToString() == animalId);

                Photo[] animalPhotos = await repository.All<Photo>(p => p.AnimalId.ToString() == animalId).ToArrayAsync();

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

                Message[] allMessages = await repository.
                    All<Message>(m => m.AnimalId.ToString() == animalId).
                    ToArrayAsync();

                repository.DeleteRange(allMessages);

                try
                {
                    await repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, string.Format(SuccessfullyDeleteAnimal, animal.Name));
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

