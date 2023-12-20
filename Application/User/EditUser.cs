namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Identity;

    using Domain;
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
            private readonly UserManager<User> userManager;

            public EditUserHandler(IRepository repository,
                                   UserManager<User> userManager)
            {
                this.repository = repository;
                this.userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                string[] roles = request.User.Roles;
                User? user = await this.repository.FirstOrDefaultAsync<User>(u => u.Id.ToString() == request.UserId.ToLower());

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                user.Name = request.User.Name;
                user.Email = request.User.Email;
                user.Address = request.User.Address;
                user.City = request.User.City;
                user.Age = request.User.Age;
                user.Education = request.User.Education;
                user.JobTitle = request.User.JobTitle;
                user.Gender = request.User.Gender;
                user.Description = request.User.Description;

                try
                {
                    foreach (var role in roles)
                    {
                        if (!await userManager.IsInRoleAsync(user, role))
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }

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
