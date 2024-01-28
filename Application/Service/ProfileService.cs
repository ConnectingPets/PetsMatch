namespace Application.Service
{
    using System.Threading.Tasks;

    using MediatR;

    using DTOs.User;
    using Interfaces;
    using Response;
    using Domain;

    using static User.DeleteUser;
    using static User.EditUser;
    using static User.UserProfile;
    using static User.DeleteRole;
    using static User.ChangePassword;

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

        public async Task<Result<Unit>> ChangePassword(
            ChangePasswordDto dto,
            User user)
        => await this.mediator.Send(new ChangePasswordCommand()
        {
            Dto = dto,
            User = user
        });
    }
}
