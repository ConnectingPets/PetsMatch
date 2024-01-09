namespace Application.Service
{
    using System.Threading.Tasks;

    using MediatR;

    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Application.Response;

    using static Application.User.DeleteUser;
    using static Application.User.EditUser;
    using static Application.User.UserProfile;
    using static Application.User.DeleteRole;

    public class ProfileService : IProfileService
    {
        private readonly IMediator mediator;

        public ProfileService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result<Unit>> DeleteUser(string userId)
            => await this.mediator.Send(new DeleteUserCommand
            {
                UserId = userId
            });

        public async Task<Result<Unit>> EditUser(string userId, EditUserDto editUserDto)
            => await this.mediator.Send(new EditUserCommand
            {
                UserId = userId,
                User = editUserDto
            });

        public async Task<Result<UserProfileDto>> GetProfile(string userId)
            => await this.mediator.Send(new UserProfileQuery
            {
                UserId = userId,
            });

        public async Task<Result<Unit>> DeleteRole(string userId, string roleName)
            => await this.mediator.Send(new DeleteRoleCommand
            {
                RoleName = roleName,
                UserId = userId
            });
    }
}
