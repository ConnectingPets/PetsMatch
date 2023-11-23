namespace Application.Photo
{
    using System.Threading.Tasks;
    using System.Threading;

    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Response;
    using Service.Interfaces;

    public class AddUserPhoto
    {
        public class AddUserPhotoCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;
            public IFormFile File { get; set; } = null!;
        }

        public class AddUserPhotoCommandHandler :
            IRequestHandler<AddUserPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;

            public AddUserPhotoCommandHandler(IPhotoService photoService)
            {
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(AddUserPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile file = request.File;

                if (file == null || file.Length == 0)
                {
                    return
                        Result<Unit>.Failure("File is not selected or empty");
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