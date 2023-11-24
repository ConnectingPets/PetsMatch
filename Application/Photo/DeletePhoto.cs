namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Persistence.Repositories;
    using Response;
    using Domain;
    using Service.Interfaces;

    public class DeletePhoto
    {
        public class DeletePhotoCommand : IRequest<Result<Unit>>
        {
            public string PublicId { get; set; } = null!;
        }

        public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public DeletePhotoCommandHandler(IPhotoService photoService,
                                             IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
            {
                string photoId = request.PublicId;
                Photo? photo = await repository.
               FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photo == null)
                {
                    return Result<Unit>.Failure("This photo does not exist! Please select existing one");
                }

                if (photo.IsMain)
                {
                    return Result<Unit>.Failure("This is your main photo! You can not delete it");
                }

                return await photoService.DeletePhotoAsync(photoId, photo);
            }
        }
    }
}
