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

                string? gender = null;
                if (user.Gender.HasValue)
                {
                    Gender enumGender = (Gender)user.Gender;
                    gender = enumGender.ToString();
                }

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
                    Photo = Convert.ToBase64String(user.Photo)
                };

                return userProfileDto;
            }
        }
    }
}
