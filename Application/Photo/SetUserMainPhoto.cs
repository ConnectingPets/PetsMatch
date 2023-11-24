namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;

    public class SetUserMainPhoto
    {
        public class SetUserMainPhotoCommand : IRequest<Result<Unit>>
        {
            public string PhotoId { get; set; } = null!;
        }

        public class SetUserMainPhotoCommandHandler : IRequestHandler<SetUserMainPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public SetUserMainPhotoCommandHandler(IPhotoService photoService,
                                                  IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(SetUserMainPhotoCommand request, CancellationToken cancellationToken)
            {
                string photoId = request.PhotoId;
                Photo? photo = await repository.
                FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photo == null)
                {
                    return Result<Unit>.Failure("This photo does not exist! Please select existing one");
                }
                return
                    await photoService.SetUserMainPhotoAsync(photoId, photo);
            }
        }
    }
}
