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
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly ITokenService tokenService;

            public LoginUserHandler(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ITokenService tokenService)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.userManager
                   .FindByEmailAsync(request.Email);

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

                    return Result<UserDto>.Success(new UserDto
                    {
                        Name = user.Name,
                        Photo = null,
                        Token = tokenService.CreateToken(user)
                    });
                }
                catch (Exception)
                {
                    return Result<UserDto>.Failure(FailedLogin);
                }
            }
        }
    }
}