using Domain.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CreateAnimalRequest : IRequest<bool>
    {
        public CreateAnimalRequest(AnimalViewModel model, string id)
        {
            Animal = model;
            UserId = id;
        }

        public Domain.ViewModels.AnimalViewModel Animal { get; set; } = null!;

        public string UserId { get; set; } 
    }

    public class CreateAnimalHandler : IRequestHandler<CreateAnimalRequest, bool>
    {
        IRepository repository;

        public CreateAnimalHandler(IRepository repository)
        {
                this.repository = repository;
        }

        public async Task<bool> Handle(CreateAnimalRequest request, CancellationToken cancellationToken)
        {
            Domain.Animal animal = new Domain.Animal()
            {
                Age = request.Animal.Age,
                BirthDate = request.Animal.BirthDate,
                Breed = request.Animal.Breed,
                SocialMedia = request.Animal.SocialMedia,
                HealthStatus = request.Animal.HealthStatus,
                Description = request.Animal.Description,
                Gender = request.Animal.Gender,
                IsEducated = request.Animal.IsEducated,
                IsHavingValidDocuments = request.Animal.IsHavingValidDocuments,
                Name = request.Animal.Name,
                OwnerId = Guid.Parse(request.UserId),
                Photo = request.Animal.Photo,
                Weight = request.Animal.Weight,
                BreedId = request.Animal.BreedId,
            };


            await repository.AddAsync(animal);

            var result = await repository.SaveChangesAsync();

            if (result > 0)
            {
                return false;
            }
                return true;      
        }
    }
}
