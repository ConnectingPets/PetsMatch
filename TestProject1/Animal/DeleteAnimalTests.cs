namespace Tests.Animal
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.Animal.DeleteAnimal;

    [TestFixture]
    public class DeleteAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private Animal animal;
        private IEnumerable<AnimalMatch> matches;
        private DeleteAnimalCommand command;
        private DeleteAnimalCommandHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            command = new DeleteAnimalCommand
            {
                AnimalId = "1c9cd1de-dba5-46f7-837e-a316447e13c0",
                UserId = "3e973314-0946-4c02-a352-0d0f2032c19d"
            };
            animal = new Animal
            {
                AnimalId = Guid.Parse("1c9cd1de-dba5-46f7-837e-a316447e13c0"),
                OwnerId = Guid.Parse("3e973314-0946-4c02-a352-0d0f2032c19d"),
                Name = "Pet1",
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 2,
                Gender = Gender.Female,
                IsEducated = false,
                IsHavingValidDocuments = false,
                HealthStatus = HealthStatus.Vaccinated
            };
            handler = new DeleteAnimalCommandHandler(repositoryMock.Object, photoServiceMock.Object);
            matches = new List<AnimalMatch>()
            {
                new AnimalMatch()
                {
                     AnimalId =
                     Guid.Parse("1c9cd1de-dba5-46f7-837e-a316447e13c0"),
                     MatchId =
                     Guid.Parse("5efc7539-f654-4425-86b2-2413b7756116")
                }
            };
        }

        [Test]
        public async Task Handle_ValidCommandAndOwner_ReturnsSuccessResult()
        {
            SetUpPhotos(repositoryMock);
            SetUpDeletingAnimal(repositoryMock, command, animal, matches);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"You have successfully delete {animal.Name} from your pet's list", result.SuccessMessage);
        }

        [Test]
        public async Task Handle_InvalidAnimalId_ReturnsFailureResult()
        {
            repositoryMock.Setup(r => r.DeleteAsync<Animal>(Guid.Parse(command.AnimalId))).
                ThrowsAsync(new InvalidOperationException());

            SetUpPhotos(repositoryMock);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_UnauthorizedUser_ReturnsFailureResult()
        {
            command = new DeleteAnimalCommand()
            {
                AnimalId = "1c9cd1de-dba5-46f7-837e-a316447e13c0",
                UserId = "0088f999-acbb-44ad-b99e-0a5f3854dc78"
            };

            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
                .ReturnsAsync(animal);

            SetUpPhotos(repositoryMock);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you!", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenSavingChanges()
        {
            SetUpDeletingAnimal(repositoryMock, command, animal, matches);
            SetUpPhotos(repositoryMock);

            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
                .ReturnsAsync(animal);
            repositoryMock.Setup(r => r.SaveChangesAsync())
                .Throws(new Exception());


            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to delete pet - {animal.Name}", result.ErrorMessage);
        }

        private static void SetUpDeletingAnimal(
            Mock<IRepository> repositoryMock,
            DeleteAnimalCommand command,
            Animal animal,
            IEnumerable<AnimalMatch> matches)
        {
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>())).
                ReturnsAsync(animal);

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            repositoryMock.Setup(r => r.
            DeleteAsync<Animal>(Guid.Parse(command.AnimalId)))
                .Returns(Task.CompletedTask);

            var queryableMatches = matches.AsQueryable();
            var asyncEnumerableMatches =
                new TestAsyncEnumerableEfCore<AnimalMatch>(queryableMatches);
            repositoryMock.
                Setup(r => r.
                All(It.IsAny<Expression<Func<AnimalMatch, bool>>>())).
                Returns(asyncEnumerableMatches);


            var queryableMessages = new List<Message>().AsQueryable();
            var asyncEnumerableMessages =
                new TestAsyncEnumerableEfCore<Message>(queryableMessages);

            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<Message, bool>>>()))
                .Returns(asyncEnumerableMessages);

            repositoryMock.Setup(r => r.DeleteAsync<Domain.Match>(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            repositoryMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        private static void SetUpPhotos(Mock<IRepository> repositoryMock)
        {
            var queryablePhoto = new List<Photo>().AsQueryable();
            var asyncEnumerablePhoto =
                new TestAsyncEnumerableEfCore<Photo>(queryablePhoto);

            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<Photo, bool>>>()))
                .Returns(asyncEnumerablePhoto);
        }
    }
}
