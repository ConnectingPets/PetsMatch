﻿namespace Application.User
{
    using Application.Exceptions;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUser
    {
        public class LoginUserCommand : IRequest<User>
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public bool RememberMe { get; set; }
        }

        public class LoginUserHandler : IRequestHandler<LoginUserCommand, User>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;

            public LoginUserHandler(
                UserManager<User> userManager,
                SignInManager<User> signInManager)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
            }

            public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.userManager
                   .FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

                if (!result.Succeeded)
                {
                    throw new UserResultNotSucceededException();
                }

                return user;
            }
        }
    }
}
