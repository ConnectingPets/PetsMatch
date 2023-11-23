﻿namespace Application.User
{
    using Application.Exceptions;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterUser
    {
        public class RegisterUserCommand : IRequest<User>
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string Name { get; set; } = null!;
        }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, User>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;

            public RegisterUserHandler(
                UserManager<User> userManager,
                SignInManager<User> signInManager)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
            }

            public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                User user = new User()
                {
                    Email = request.Email,
                    Name = request.Name,
                    UserName = request.Email,
                };

                IdentityResult result =
                    await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new UserResultNotSucceededException();
                }

                await signInManager.SignInAsync(user, false);

                return user;
            }
        }
    }
}
