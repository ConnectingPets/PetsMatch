namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.DTOs.User;
    using Application.Response;

    using static Common.ExceptionMessages.User;
    using static Common.FailMessages.User;
    using static Common.SuccessMessages.User;

    public class EditUser
    {
        public class EditUserCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;

            public EditUserDto User { get; set; } = null!;
        }

        public class EditUserHandler : IRequestHandler<EditUserCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public EditUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository.GetById<User>(Guid.Parse(request.UserId));

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                Gender? gender = null;
                if (request.User.Gender != null)
                {
                    if (!Enum.TryParse(request.User.Gender, out Gender enumValue))
                    {
                        return Result<Unit>.Failure(InvalidGender);
                    }

                    gender = enumValue;
                }

                user.Name = request.User.Name;
                user.Email = request.User.Email;
                user.Address = request.User.Address;
                user.City = request.User.City;
                user.Age = request.User.Age;
                user.Education = request.User.Education;
                user.JobTitle = request.User.JobTitle;
                user.Gender = gender;
                user.Description = request.User.Description;

                try
                {
                    await this.repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessEditUser, user.Name));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(FailedEditUser, user.Name));
                }
            }
        }
    }
}
