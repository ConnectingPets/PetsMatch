namespace Tests.Animal
{
    using Moq;
    using MediatR;

    using Domain;
    using Domain.Enum;
    using Application.Animal;
    using Application.DTOs.Animal;
    using Persistence.Repositories;
    using Application.Service.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class AddAnimalTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var mediatorMock = new Mock<IMediator>();
            var photoService = new Mock<IPhotoService>();

            var handler = new AddAnimal.AddAnimalCommandHandler(repositoryMock.Object, photoService.Object);

            var command = new AddAnimal.AddAnimalCommand
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

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That($"You have successfully add {command.AnimalDto.Name} to your pet's list", Is.EqualTo(result.SuccessMessage));
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_FailedToCreatePet_ReturnsFailureResult()
        {
            // Arrange
            //var repositoryMock = new Mock<IRepository>();
            //var photoService = new Mock<IPhotoService>();
            //repositoryMock.Setup(r => r.SaveChangesAsync()).Throws(new Exception("Simulating save failure"));

            //var handler = new AddAnimal.AddAnimalCommandHandler(repositoryMock.Object, photoService.Object);

            //var command = new AddAnimal.AddAnimalCommand
            //{
            //    AnimalDto = new AddAnimalDto()
            //    {
            //        Name = "Test",
            //        Age = 1,
            //        Description = "Test description",
            //        IsHavingValidDocuments = true,
            //        Gender = Gender.Male,
            //        HealthStatus = HealthStatus.Vaccinated,
            //        IsEducated = true,
            //        BreedId = 1,
            //    },
            //    OwnerId = "F6E0FC1A-7726-4519-A599-0114A1EB1875"
            //};

            //// Act
            //var result = await handler.Handle(command, CancellationToken.None);

            //// Assert
            //Assert.IsFalse(result.IsSuccess);
            //Assert.AreEqual($"Failed to create pet - {command.AnimalDto.Name}", result.Error);
            //repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            //repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
