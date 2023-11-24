namespace Application.Photo
{
    using System.Threading.Tasks;
    using System.Threading;

    using MediatR;

    using Response;
    using Service.Interfaces;

    public class SetAnimaMainPhoto
    {
        public class SetAnimalMainPhotoCommand : IRequest<Result<Unit>>
        {
            public string PhotoId { get; set; } = null!;
        }

        public class SetAnimalMainPhotoCommandHandler : IRequestHandler<SetAnimalMainPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;

            public SetAnimalMainPhotoCommandHandler(IPhotoService photoService)
            {
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(SetAnimalMainPhotoCommand request, CancellationToken cancellationToken)
            {
                return await photoService.SetAnimalMainPhotoAsync(request.PhotoId);
            }
        }
    }
}
