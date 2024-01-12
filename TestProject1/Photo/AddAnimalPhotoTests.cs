namespace Tests.Photo
{
    using System.Linq.Expressions;

    using MediatR;
    using Microsoft.AspNetCore.Http;
    using MockQueryable.EntityFrameworkCore;

    using Moq;
    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Service.Interfaces;
    
    using static Application.Photo.AddAnimalPhoto;

    [TestFixture]
    public class AddAnimalPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private AddAnimalPhotoCommandHandler handler;
        private Animal animal;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new AddAnimalPhotoCommandHandler(photoServiceMock.Object, repositoryMock.Object);
            animal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 2,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "test",
                OwnerId = Guid.NewGuid(),
            };
        }

        [Test]
        public async Task Handler_ShouldAddPhoto_WhenGivingCorrectData()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            var command = SetUpReturningPhoto();

            photoServiceMock.Setup(ps => ps.
            AddAnimalPhotosAsync(command.Files, animal)).
            ReturnsAsync(Result<Unit>.
            Success(Unit.Value, "Successfully upload photo"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Successfully upload photo", result.SuccessMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotosIsNull()
        {
            var result = await handler.
                Handle(new AddAnimalPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("File is not selected or empty", result.ErrorMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotosArrayIsEmpty()
        {
            var command = new AddAnimalPhotoCommand()
            {
                 Files = Array.Empty<IFormFile>()
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("File is not selected or empty", result.ErrorMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenAnimalDoesNotExist()
        {
            var command = SetUpReturningPhoto();
            SetUpReturningAnimal(repositoryMock, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        private static void SetUpReturningAnimal(
            Mock<IRepository> repositoryMock,
            Animal animal)
        {
            var queryable =
                new List<Animal> { animal }.AsQueryable();
            var asyncEnumerable =
                 new TestAsyncEnumerableEfCore<Animal>(queryable);
            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<Animal, bool>>>()))
                .Returns(asyncEnumerable);
        }

        private static AddAnimalPhotoCommand SetUpReturningPhoto()
        {
            var formFileMock = new Mock<IFormFile>();

            var command = new AddAnimalPhotoCommand()
            {
                AnimalId = Guid.NewGuid().ToString(),
                Files =
                   [
                       formFileMock.Object
                   ]
            };

            return command;
        }
    }
}
