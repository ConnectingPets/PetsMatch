namespace Tests.Animal
{
    using Moq;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.DTOs.Animal;

    using static Application.Animal.EditAnimal;

    [TestFixture]
    public class EditAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private EditAnimalCommand command;
        private Animal animal;
        private EditAnimalCommandHandler handler;

        [SetUp]
        public void SetUp()
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
                    Name = "Pet1",
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
            handler = new EditAnimalCommandHandler(repositoryMock.Object);
        }
        [Test]
        public async Task Handle_ValidCommandAndOwner_ReturnsSuccessResult()
        {
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

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you!", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenGuidIsNotInRightFormat()
        {
            command.AnimalId = "123";

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This is invalid Guid format", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenSavingChanges()
        {
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);
            repositoryMock.Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new Exception());

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to edit pet - {animal.Name}", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenTryingToEditNameTwice_InLessThan30Days()
        {
            command.AnimalDto.Name = "New name";
            animal.LastModifiedName = DateTime.UtcNow.AddDays(-1);
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            var result =
                await handler.Handle(command, CancellationToken.None);
            int daysDifference =
                30 - (DateTime.UtcNow - animal.LastModifiedName).Days;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Can not update pet name for another {daysDifference} days.", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenTryingToEditBreedTwice_InLessThan30Days()
        {
            command.AnimalDto.BreedId = 1;
            animal.LastModifiedBreed = DateTime.UtcNow.AddDays(-1);
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            var result =
                await handler.Handle(command, CancellationToken.None);
            int daysDifference = 30 - (DateTime.UtcNow - animal.LastModifiedBreed).Days;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Can not update pet breed for another {daysDifference} days.", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenTryingToEditGenderTwice_InLessThan30Days()
        {
            command.AnimalDto.Gender = Gender.Female;
            animal.LastModifiedGender = DateTime.UtcNow.AddDays(-1);
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            var result =
                await handler.Handle(command, CancellationToken.None);
            int daysDifference = 30 - (DateTime.UtcNow - animal.LastModifiedGender).Days;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Can not update pet gender for another {daysDifference} days.", result.ErrorMessage);
        }

        [Test]
        public async Task HandleShouldSet_LastModifiedName_WhenChangingName()
        {
            command.AnimalDto.Name = "New name";
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            await handler.Handle(command, CancellationToken.None);

            Assert.AreEqual(animal.LastModifiedName.Date, DateTime.UtcNow.Date);
        }

        [Test]
        public async Task HandleShouldSet_LastModifiedBreed_WhenChangingBreed()
        {
            command.AnimalDto.BreedId = 1;
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            await handler.Handle(command, CancellationToken.None);

            Assert.AreEqual(animal.LastModifiedBreed.Date, DateTime.UtcNow.Date);
        }

        [Test]
        public async Task HandleShouldSet_LastModifiedGender_WhenChangingGender()
        {
            command.AnimalDto.Gender = Gender.Female;
            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            await handler.Handle(command, CancellationToken.None);

            Assert.AreEqual(animal.LastModifiedGender.Date, DateTime.UtcNow.Date);
        }
    }
}
