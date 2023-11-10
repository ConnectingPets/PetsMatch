namespace Application.Animal
{
    using MediatR;

    using Service.Interfaces;
    using DTOs;
    public class AddAnimal
    {
        public class AddAnimalCommand : IRequest<Unit>
        {
            public AddAnimalCommand(AddAnimalDto animalDto)
            {
                AnimalDto = animalDto;
            }

            public AddAnimalDto AnimalDto { get; set; }
        }

        public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand, Unit>
        {
            private readonly IAnimalService animalService;

            public AddAnimalCommandHandler(IAnimalService animalService)
            {
                this.animalService = animalService;
            }

            public Task<Unit> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
            {
                return null;
                // animalService.AddAnimalAsync();
            }
        }
    }
}
