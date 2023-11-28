namespace Application.User
{
    using Application.DTOs.User;
    using Application.Exceptions;
    using Domain;
    using Domain.Enum;
    using MediatR;
    using Persistence.Repositories;
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

                Gender? gender = GetGender(request.User.Gender);

                user.Name = request.User.Name;
                user.Email = request.User.Email;
                user.Address = request.User.Address;
                user.City = request.User.City;
                user.Age = request.User.Age;
                user.Education = request.User.Education;
                user.JobTitle = request.User.JobTitle;
                user.Gender = gender;
                user.Description = request.User.Description;
                user.Photo = null;

                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }

            private Gender? GetGender(string? gender)
            {
                if (gender == null)
                {
                    return null;
                }

                if (!Enum.TryParse(gender, out Gender enumValue))
                {
                    throw new InvalidEnumException(nameof(Gender));
                }

                return enumValue;
            }
        }
    }
}
