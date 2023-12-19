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

    public class SetAnimaMainPhoto
    {
        public class SetAnimalMainPhotoCommand : IRequest<Result<Unit>>
        {
            public string PhotoId { get; set; } = null!;
        }

        public class SetAnimalMainPhotoCommandHandler : IRequestHandler<SetAnimalMainPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public SetAnimalMainPhotoCommandHandler(
                IPhotoService photoService,
                IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(SetAnimalMainPhotoCommand request, CancellationToken cancellationToken)
            {
                string photoId = request.PhotoId;
                Photo? photo = await repository.
                    FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photo == null)
                {
                    return Result<Unit>.Failure(PhotoNotExist);
                }
                return await photoService.
                    SetAnimalMainPhotoAsync(photo);
            }
        }
    }
}
