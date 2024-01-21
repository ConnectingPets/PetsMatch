namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.User;
    using Application.Response;
    using Application.Service.Interfaces;

    using static Common.ExceptionMessages.User;

    public class UserProfile
    {
        public class UserProfileQuery : IRequest<Result<UserProfileDto>>
        {
            public string UserId { get; set; } = null!;
        }

        public class UserProfileHandler : IRequestHandler<UserProfileQuery, Result<UserProfileDto>>
        {
            private readonly IRepository repository;
            private readonly ITokenService tokenService;
            private readonly UserManager<User> userManager;

            public UserProfileHandler(IRepository repository,
                                      ITokenService tokenService,
                                      UserManager<User> userManager)
            {
                this.repository = repository;
                this.tokenService = tokenService;
                this.userManager = userManager;
            }

            public async Task<Result<UserProfileDto>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
            {
                User? user = await this.repository
                    .All<User>(u => u.Id.ToString() == request.UserId.ToLower())
                    .Include(u => u.Photo)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<UserProfileDto>.Failure(UserNotFound);
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
                    Photo = user.Photo?.Url,
                    Description = user.Description,
                    Token = this.tokenService.CreateToken(user),
                    Roles = await userManager.GetRolesAsync(user)
                };

                return Result<UserProfileDto>.Success(userProfileDto);
            }
        }
    }
}
