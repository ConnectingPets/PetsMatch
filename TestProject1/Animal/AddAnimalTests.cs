namespace Tests.Animal
{
    using Moq;

    using Domain;
    using Domain.Enum;
    using Application.DTOs.Animal;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.Animal.AddAnimal;

    [TestFixture]
    public class AddAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoService;
        private AddAnimalCommand command;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            photoService = new Mock<IPhotoService>();

            command = new AddAnimalCommand
            {
                AnimalDto = new AddAnimalDto()
                {
                    Name = "Test",
                    Age = 1,
                    Description = "Test description",
                    IsHavingValidDocuments = true,
                    Gender = Gender.Male,
                    HealthStatus = HealthStatus.Vaccinated,
                    IsEducated = true,
                    BreedId = 1,
                },
                OwnerId = "F6E0FC1A-7726-4519-A599-0114A1EB1875"
            };
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            var handler = new AddAnimalCommandHandler(repositoryMock.Object, photoService.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"You have successfully add {command.AnimalDto.Name} to your pet's list", result.SuccessMessage);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_FailedToCreatePet_ReturnsFailureResult()
        {
            repositoryMock.Setup(r => r.SaveChangesAsync()).Throws(new Exception("Simulating save failure"));

            var handler = new AddAnimalCommandHandler(repositoryMock.Object, photoService.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to create pet - {command.AnimalDto.Name}", result.ErrorMessage);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
