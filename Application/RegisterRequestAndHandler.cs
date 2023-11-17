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
        public RegisterRequest(RegisterUserViewModel user)
        {
            User = user;
        }

        public RegisterUserViewModel User { get; set; } = null!;
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
                Address = request.User.Address,
                Age = request.User.Age,
                Email = request.User.Email,
                City = request.User.City,
                Description = request.User.Description,
                Education = request.User.Education,
                Gender = request.User.Gender,
                JobTitle = request.User.JobTitle,
                Name = request.User.Name,
                Photo = request.User.Photo,
                Animals = request.User.Animals,
                UsersPassions = request.User.UsersPassions
            };

            await userManager.SetEmailAsync(user, user.Email);

            await userManager.SetUserNameAsync(user, user.Name);

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