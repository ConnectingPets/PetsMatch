namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.User;
    using Application.Exceptions.Entity;
    using Application.Exceptions.User;

    public class UserProfile
    {
        public class UserProfileQuery : IRequest<UserProfileDto>
        {
            public string UserId { get; set; } = null!;
        }

        public class UserProfileCommand : IRequestHandler<UserProfileQuery, UserProfileDto>
        {
            private readonly IRepository repository;

            public UserProfileCommand(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<UserProfileDto> Handle(UserProfileQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.UserId, out Guid guidUserId))
                {
                    throw new InvalidGuidFormatException();
                }

                User? user = await this.repository
                    .All<User>(u => u.Id == guidUserId)
                    .Include(u => u.Photo)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                string? gender = user.Gender.HasValue
                    ? user.Gender.ToString()
                    : null;

                UserProfileDto userProfileDto = new UserProfileDto
                {
                    Name = user.Name,
                    Email = user.Email!,
                    Age = user.Age,
                    Gender = gender,
                    Address = user.Address,
                    City = user.City,
                    Education = user.Education,
                    JobTitle = user.JobTitle,
                    Photo = user.Photo?.Url
                };

                return userProfileDto;
            }
        }
    }
}
