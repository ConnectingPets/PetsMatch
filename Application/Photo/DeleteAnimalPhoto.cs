namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Photo;

    public class DeleteAnimalPhoto
    {
        public class DeleteAnimalPhotoCommand : IRequest<Result<Unit>>
        {
            public string PublicId { get; set; } = null!;
        }

        public class DeleteAnimalPhotoCommandHandler : IRequestHandler<DeleteAnimalPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public DeleteAnimalPhotoCommandHandler(IPhotoService photoService,
                                             IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteAnimalPhotoCommand request, CancellationToken cancellationToken)
            {
                string photoId = request.PublicId;
                Photo? photo = await repository.
               FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photo == null)
                {
                    return Result<Unit>.Failure(PhotoNotExist);
                }

                if (photo.IsMain)
                {
                    return Result<Unit>.Failure(MainPhotoError);
                }

                return await photoService.DeleteAnimalPhotoAsync(photo);
            }
        }
    }
}
