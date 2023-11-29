namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    using Domain;
    using Application.DTOs.User;
    using Application.Exceptions;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Exceptions.Entity;

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

                User? user = await this.repository.GetById<User>(guidUserId);

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
                    Photo = null
                };

                return userProfileDto;
            }
        }
    }
}
