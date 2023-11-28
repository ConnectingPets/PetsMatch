namespace Application.User
{
    using Application.DTOs.User;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Application.Exceptions;
    using Persistence.Repositories;

    public class EditUser
    {
        public class EditUserCommand : IRequest<Unit>
        {
            public string UserId { get; set; } = null!;

            public EditUserDto User { get; set; } = null!;
        }

        public class EditUserHandler : IRequestHandler<EditUserCommand, Unit>
        {
            private readonly IRepository repository;

            public EditUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.UserId, out Guid guidUserId))
                {
                    throw new InvalidGuidFormatException();
                }

                User? user = await this.repository.GetById<User>(guidUserId);

                if (user == null)
                {
                    throw new UserNotFoundException();
                }



                return Unit.Value;
            }
        }
    }
}
