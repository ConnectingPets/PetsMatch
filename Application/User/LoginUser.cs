namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    using Domain;
    using Application.Response;
    using Application.DTOs.User;
    using Application.Service.Interfaces;

    using static Common.ExceptionMessages.User;
    using static Common.FailMessages.User;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class LoginUser
    {
        public class LoginUserCommand : IRequest<Result<UserDto>>
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public bool RememberMe { get; set; }
        }

        public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
        {
            private readonly SignInManager<User> signInManager;
            private readonly IRepository repository;
            private readonly ITokenService tokenService;

            public LoginUserHandler(
                SignInManager<User> signInManager,
                IRepository repository,
                ITokenService tokenService)
            {
                this.repository = repository;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository
                    .All<User>(u => u.Email == request.Email)
                    .Include(u => u.Photo)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<UserDto>.Failure(UserNotFound);
                }

                try
                {
                    SignInResult result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

                    if (!result.Succeeded)
                    {
                        return Result<UserDto>.Failure(FailedLogin);
                    }

                    UserDto userDto = new UserDto
                    {
                        Name = user.Name,
                        PhotoUrl = user.Photo?.Url,
                        Token = this.tokenService.CreateToken(user),
                        City = user.City,
                        Address = user.Address,
                        JobTitle = user.JobTitle,
                        Gender = user.Gender.ToString(),
                        Education = user.Education,
                        Age = user.Age
                    };

                    return Result<UserDto>.Success(userDto);
                }
                catch (Exception)
                {
                    return Result<UserDto>.Failure(FailedLogin);
                }
            }
        }
    }
}