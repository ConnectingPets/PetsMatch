namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Response;
    using Service.Interfaces;

    public class SetUserMainPhoto
    {
        public class SetUserMainPhotoCommand : IRequest<Result<Unit>>
        {
            public string PhotoId { get; set; } = null!;
        }

        public class SetUserMainPhotoCommandHandler : IRequestHandler<SetUserMainPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;

            public SetUserMainPhotoCommandHandler(IPhotoService photoService)
            {
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(SetUserMainPhotoCommand request, CancellationToken cancellationToken)
            {
                return await photoService.SetUserMainPhotoAsync(request.PhotoId);
            }
        }
    }
}
