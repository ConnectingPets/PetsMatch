namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using Response;
    using Persistence.Repositories;

    using static Common.FailMessages.User;
    using static Common.SuccessMessages.User;
    using static Common.ExceptionMessages.User;

    public class DeleteRole
    {
        public class DeleteRoleCommand : IRequest<Result<Unit>>
        {
            public string RoleName { get; set; } = null!;
            public string UserId { get; set; } = null!;
        }

        public class DeleteRoleHandler :
            IRequestHandler<DeleteRoleCommand, Result<Unit>>
        {
            private readonly IRepository repository;
            private readonly UserManager<User> userManager;

            public DeleteRoleHandler(IRepository repository,
                                     UserManager<User> userManager)
            {
                this.repository = repository;
                this.userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
            {
                string roleName = request.RoleName;
                User? user = await repository.FirstOrDefaultAsync<User>(u => u.Id.ToString() == request.UserId);

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                var roles = await userManager.GetRolesAsync(user);

                if (roles.Count() == 1)
                {
                    return Result<Unit>.
                        Failure(string.Format(FailedDeleteRole, roleName));
                }

                try
                {
                    await userManager.RemoveFromRoleAsync(user, roleName);
                    return Result<Unit>.Success(Unit.Value, string.Format(SuccessDeleteRole, roleName));
                }
                catch (Exception)
                {
                    return Result<Unit>.
                        Failure(string.Format(FailedDeleteRole, roleName));
                }
            }
        }
    }
}
