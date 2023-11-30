namespace Application.Service
{
    using System.Threading.Tasks;
    
    using MediatR;
    
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using static Application.User.DeleteUser;
    using static Application.User.EditUser;
    using static Application.User.UserProfile;

    public class ProfileService : IProfileService
    {
        private readonly IMediator mediator;

        public ProfileService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task DeleteUser(string userId)
            => await this.mediator.Send(new DeleteUserCommand
            {
                UserId = userId
            });

        public async Task EditUser(string userId, EditUserDto editUserDto)
            => await this.mediator.Send(new EditUserCommand
            {
                UserId = userId,
                User = editUserDto
            });

        public async Task<UserProfileDto> GetProfile(string userId)
            => await this.mediator.Send(new UserProfileQuery
            {
                UserId = userId,
            });
    }
}
