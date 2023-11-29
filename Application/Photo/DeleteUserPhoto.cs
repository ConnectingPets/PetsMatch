namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Persistence.Repositories;
    using Response;
    using Domain;
    using Service.Interfaces;

    public class DeleteUserPhoto
    {
        public class DeleteUserPhotoCommand : IRequest<Result<Unit>>
        {
            public string PublicId { get; set; } = null!;
            public string UserId { get; set; } = null!;
        }

        public class DeleteUserPhotoCommandHandler : IRequestHandler<DeleteUserPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public DeleteUserPhotoCommandHandler(IPhotoService photoService,
                                             IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
            {
                string photoId = request.PublicId;
                Photo? photo = await repository.
               FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photo == null)
                {
                    return Result<Unit>.Failure("This photo does not exist! Please select existing one");
                }

                return await photoService.DeleteUserPhotoAsync(photoId, photo, request.UserId);
            }
        }
    }
}
