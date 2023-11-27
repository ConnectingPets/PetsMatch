namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    using Domain;
    using Application.DTOs.User;
    using Application.Exceptions;

    public class UserProfile
    {
        public class UserProfileQuery : IRequest<UserProfileDto>
        {
            public string UserId { get; set; } = null!;
        }

        public class UserProfileCommand : IRequestHandler<UserProfileQuery, UserProfileDto>
        {
            private readonly UserManager<User> userManager;

            public UserProfileCommand(UserManager<User> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<UserProfileDto> Handle(UserProfileQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.UserId, out Guid guidUserId))
                {
                    throw new InvalidGuidFormatException();
                }

                User? user = await this.userManager.FindByIdAsync(request.UserId);

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                UserProfileDto userProfileDto = new UserProfileDto
                {

                };

                return userProfileDto;
            }
        }
    }
}
