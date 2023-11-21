﻿using Domain;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class RegisterRequest : IRequest<Domain.User>
    {
        public RegisterRequest(RegisterUserDto user)
        {
            User = user;
        }

        public RegisterUserDto User { get; set; } = null!;
    }

    public class RegisterHandler : IRequestHandler<RegisterRequest, Domain.User>
    {
        private readonly UserManager<Domain.User> userManager;

        public RegisterHandler(UserManager<Domain.User> userManager)
        {

            this.userManager = userManager;
        }


        public async Task<Domain.User> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {


            Domain.User user = new Domain.User()
            {
                Address = string.Empty,
                Age = 0,
                Email = request.User.Email,
                Education = string.Empty,
                City = string.Empty,
                JobTitle = string.Empty,
                Name = request.User.Name,
                Gender = Domain.Enum.Gender.Male,
                UserName = request.User.Email
            };

            IdentityResult result =
                await userManager.CreateAsync(user, request.User.Password);

            if (!result.Succeeded)
            {
                return null;
            }


            return user;


        }
    }
}