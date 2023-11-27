namespace Application.User
{
    using Application.DTOs.User;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditUser
    {
        public class EditUserCommand : IRequest<Unit>
        {
            public string UserId { get; set; } = null!;

            public EditUserDto User { get; set; } = null!;
        }

        public class EditUserHandler : IRequestHandler<EditUserCommand, Unit>
        {
            public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}
