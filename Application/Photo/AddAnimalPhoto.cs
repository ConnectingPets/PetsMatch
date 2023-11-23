namespace Application.Photo
{
    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Response;
    using Service.Interfaces;

    public class AddAnimalPhoto
    {
        public class AddAnimalPhotoCommand : IRequest<Result<Unit>>
        {
            public string AnimalId { get; set; } = null!;
            public IFormFile File { get; set; } = null!;
        }

        public class AddAnimalPhotoCommandHandler :
            IRequestHandler<AddAnimalPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;

            public AddAnimalPhotoCommandHandler(IPhotoService photoService)
            {
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(AddAnimalPhotoCommand request, CancellationToken cancellationToken)
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

                var result = await photoService.AddAnimalPhotoAsync(file, request.AnimalId);

                return result;
            }
        }
    }
}
