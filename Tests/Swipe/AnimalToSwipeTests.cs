namespace Tests.Swipe
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Swipe.AnimalsToSwipe;

    [TestFixture]
    public class AnimalToSwipeTests
    {
        private Mock<IRepository> repositoryMock;
        private AnimalsToSwipeHandler handler;
        private AnimalsToSwipeQuery command;
        private Animal myAnimal;
        private Animal notMyAnimal;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AnimalsToSwipeHandler(repositoryMock.Object);
            myAnimal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test",
                OwnerId = Guid.Parse("0e48736b-7b3a-41cb-ac14-e22e3d77571b"),
            };
            command = new AnimalsToSwipeQuery()
            {
                UserId = myAnimal.OwnerId.ToString()
            };
            notMyAnimal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Another animal",
                OwnerId = Guid.NewGuid(),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                         Id = "Id",
                         IsMain = true,
                         Url = "Url"
                    }
                },
                Breed = new Breed()
                {
                    Name = "Test breed",
                    CategoryId = 1,
                }
            };
        }

        [Test]
        public async Task Handle_ReturnsSuccessResult_WhenGivingCorrectData()
        {
            SetUpReturningAnimal(repositoryMock, myAnimal);
            SetUpAnyAsyncReturnsTrue<User>(repositoryMock);

            var queryable = new List<Animal> { notMyAnimal }.AsQueryable();
            var asyncEnumerable =
                 new TestAsyncEnumerableEfCore<Animal>(queryable);

            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<Animal, bool>>>()))
                .Returns(asyncEnumerable);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(notMyAnimal.Name, result.Data.First().Name);
            Assert.AreEqual(notMyAnimal.Breed.Name, result.Data.First().Breed);
            Assert.AreEqual(notMyAnimal.Photos.First().Url, result.Data.First().Photos.First().Url);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenAnimalIsNotFound()
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenUserIsNotFound()
        {
            SetUpReturningAnimal(repositoryMock, myAnimal);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenUserIsNotOwner()
        {
            SetUpReturningAnimal(repositoryMock, myAnimal);
            SetUpAnyAsyncReturnsTrue<User>(repositoryMock);
            command.UserId = Guid.NewGuid().ToString();

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        private static void SetUpAnyAsyncReturnsTrue<T>(
            Mock<IRepository> repositoryMock) where T : class
        {
            repositoryMock.Setup(r => r.
            AnyAsync(It.IsAny<Expression<Func<T, bool>>>())).
                ReturnsAsync(true);
        }

        private static void SetUpReturningAnimal(
            Mock<IRepository> repositoryMock,
            Animal animal)
        {
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
                .ReturnsAsync(animal);
        }
    }
}
