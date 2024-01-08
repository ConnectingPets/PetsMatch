namespace Tests.Animal
{
    using Application.Animal;
    using Application.DTOs.Animal;
    using Domain.Enum;
    using Moq;
    using Persistence.Repositories;
    using static Application.Animal.EditAnimal;
    using Domain;

    [TestFixture]
    public class EditAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private EditAnimalCommand command;
        private Animal animal;

        public EditAnimalTests()
        {
            repositoryMock = new Mock<IRepository>();
            command = new EditAnimalCommand
            {
                AnimalDto = new EditAnimalDto()
                {
                    Age = 1,
                    BreedId = 2,
                    Gender = Gender.Male,
                    HealthStatus = HealthStatus.Vaccinated,
                    IsEducated = false,
                    IsHavingValidDocuments = false,
                    Name = "Test",
                },
                AnimalId = "ae159b2f-6c40-4645-b723-9144b73d2004",
                UserId = "131ab0c7-2f5e-4414-bd24-42a6763654f0"
            };
            animal = new Animal
            {
                Name = "Pet1",
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 2,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                AnimalId = Guid.Parse("ae159b2f-6c40-4645-b723-9144b73d2004"),
                OwnerId = Guid.Parse("131ab0c7-2f5e-4414-bd24-42a6763654f0"),
            };
        }
        [Test]
        public async Task Handle_ValidCommandAndOwner_ReturnsSuccessResult()
        {
            var handler = new EditAnimalCommandHandler(repositoryMock.Object);

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);
            repositoryMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"Successfully updated {animal.Name}", result.SuccessMessage);
        }

        [Test]
        public async Task Handle_InvalidAnimalId_ReturnsFailureResult()
        {
            var handler = new EditAnimalCommandHandler(repositoryMock.Object);

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync((Animal)null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_UnauthorizedUser_ReturnsFailureResult()
        {
            command.UserId = "4b4996d2-ec7f-4357-9091-23d4be685f1f";
            var handler = new EditAnimalCommandHandler(repositoryMock.Object);

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you!", result.ErrorMessage);
        }
    }
}
