namespace Application.Photo
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Response;
    using Service.Interfaces;

    public class DeletePhoto
    {
        public class DeletePhotoCommand : IRequest<Result<Unit>>
        {
            public string publicId { get; set; } = null!;
        }

        public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;

            public DeletePhotoCommandHandler(IPhotoService photoService)
            {
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
            {
                return await photoService.DeletePhotoAsync(request.publicId);
            }
        }
    }
}
