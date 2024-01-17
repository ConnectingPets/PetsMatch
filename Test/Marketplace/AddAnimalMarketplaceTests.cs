namespace Tests.Marketplace
{
    using Moq;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Service.Interfaces;
    using Application.DTOs.Marketplace;

    using static Application.Marketplace.AddAnimalMarketplace;

    [TestFixture]
    public class AddAnimalMarketplaceTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private AddAnimalMarketplaceCommand command;
        private AddMarketplaceAnimalCommandHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new AddMarketplaceAnimalCommandHandler(
                repositoryMock.Object, photoServiceMock.Object);
            command = new AddAnimalMarketplaceCommand()
            {
                OwnerId = "7580dc0b-0d13-4edc-9c9a-b12a2a54a652",
                AnimalDto = new AddAnimalMarketplaceDto()
                {
                    Age = 1,
                    BreedId = 1,
                    Gender = Gender.Female,
                    HealthStatus = HealthStatus.Dewormed,
                    IsEducated = false,
                    IsHavingValidDocuments = false,
                    Name = "Test",
                    IsForSale = true,
                }
            };
        }


        [Test]
        public async Task Handle_ValidCommand_WithAnimalForSale_ReturnsSuccessResult()
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"You have successfully add {command.AnimalDto.Name} to your pet's list", result.SuccessMessage);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ValidCommand_WithAnimalForAdoption_ReturnsSuccessResult()
        {
            command.AnimalDto.IsForSale = false;
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"You have successfully add {command.AnimalDto.Name} to your pet's list", result.SuccessMessage);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_FailedToCreatePet_ReturnsFailureResult()
        {
            repositoryMock.Setup(r => r.SaveChangesAsync()).
                Throws(new Exception("Simulating save failure"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to create pet - {command.AnimalDto.Name}", result.ErrorMessage);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Animal>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
