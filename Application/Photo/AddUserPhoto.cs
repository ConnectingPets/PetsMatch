namespace Application.Photo
{
    using System.Threading.Tasks;
    using System.Threading;

    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;

    public class AddUserPhoto
    {
        public class AddUserPhotoCommand : IRequest<Result<Unit>>
        {
            public IFormFile File { get; set; } = null!;
            public string UserId { get; set; } = null!;
        }

        public class AddUserPhotoCommandHandler :
            IRequestHandler<AddUserPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public AddUserPhotoCommandHandler(IPhotoService photoService,
                                              IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(AddUserPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile file = request.File;
                User? user = await repository.
                    FirstOrDefaultAsync<User>(u => u.Id.ToString() == request.UserId);

                if (user!.PhotoId != null)
                {
                    return Result<Unit>.Failure("You already have photo!");
                }

                if (file == null || file.Length == 0)
                {
                    return Result<Unit>.Failure("File is not selected or empty");
                }

                if (!file.ContentType.StartsWith("image"))
                {
                    return
                        Result<Unit>.Failure("This file is not an image");
                }

                var result = 
                    await photoService.AddUserPhotoAsync(file, request.UserId);

                return result;
            }
        }
    }
}