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

    using static Common.FailMessages.User;

    public class RegisterUser
    {
        public class RegisterUserCommand : IRequest<Result<UserDto>>
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string Name { get; set; } = null!;
        }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly ITokenService tokenService;

            public RegisterUserHandler(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ITokenService tokenService)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                User user = new User()
                {
                    Email = request.Email,
                    Name = request.Name,
                    UserName = request.Email,
                };

                try
                {
                    IdentityResult result =
                        await userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        return Result<UserDto>.Failure(FailedRegister);
                    } 

                    await signInManager.SignInAsync(user, false);

                    return Result<UserDto>.Success(new UserDto
                    {
                        Name = user.Name,
                        Photo = null,
                        Token = tokenService.CreateToken(user)
                    });
                }
                catch (Exception)
                {
                    return Result<UserDto>.Failure(FailedRegister);
                }
            }
        }
    }
}
