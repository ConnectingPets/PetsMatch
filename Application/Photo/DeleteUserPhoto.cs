namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Service.Interfaces;
    using Response;

    public class DeleteUserPhoto
    {
        public class DeleteUserPhotoCommand : IRequest<Result<Unit>>
        {
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
                string userId = request.UserId;

                User? user = await repository.
               FirstOrDefaultAsync<User>(u => u.Id.ToString() == userId);
                string photoId = user!.PhotoId!;

                Photo? photo = await repository.FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

                if (photoId == null)
                {
                    return Result<Unit>.Failure("You don't have photo yet!");
                }

                if (photo == null)
                {
                    return Result<Unit>.Failure("This photo does not exist! Please select existing one");
                }

                return 
                    await photoService.DeleteUserPhotoAsync(photo, userId);
            }
        }
    }   
}
