namespace Tests.Swipe
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Swipe.SwipeAnimal;

    [TestFixture]
    public class SwipeAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private SwipeAnimalCommand command;
        private SwipeAnimalHandler handler;
        private Animal swiperAnimal;
        private Animal swipeeAnimal;

        [SetUp]
        public void SetUp()
        {
            this.repositoryMock = new Mock<IRepository>();
            handler = new SwipeAnimalHandler(repositoryMock.Object);
            swipeeAnimal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Swipee animal",
                OwnerId = Guid.NewGuid(),
            };
            swiperAnimal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Swiper animal",
                OwnerId = Guid.NewGuid(),
                SwipesFrom = new List<Swipe>()
                {
                   new Swipe()
                   {
                        SwipedRight = true,
                        SwiperAnimalId = swipeeAnimal.AnimalId,
                        SwipeeAnimalId = Guid.NewGuid(),
                   }
                }
            };
            command = new SwipeAnimalCommand()
            {
                SwipedRight = true,
                SwipeeAnimalId = swipeeAnimal.AnimalId.ToString(),
                SwiperAnimalId = swiperAnimal.AnimalId.ToString(),
                UserId = swiperAnimal.OwnerId.ToString(),

            };
        }

        [Test]
        public async Task Handle_ShouldReturnTrue_WhenSwipeIsMatch()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);
            SetUpReturningUser(repositoryMock);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Data);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Swipe>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFalse_WhenSwipeIsNotMatch()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);
            SetUpReturningUser(repositoryMock);

            command.SwipedRight = false;
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.Data);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Swipe>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenSwiperAnimalIsNotFound()
        {
            var queryable = new List<Animal> {}.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenSwipeeAnimalIsNotFound()
        {
            var queryable = new List<Animal> {swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenUserIsNotFound()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenIsSameAnimal()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);
            SetUpReturningUser(repositoryMock);

            command.SwipeeAnimalId = command.SwiperAnimalId;
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animal is swiping on itself", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenUserIsNotOwner()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);
            SetUpReturningUser(repositoryMock);

            command.UserId = Guid.NewGuid().ToString();
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_FailedToCreateSwipe_ReturnsFailureResult()
        {
            var queryable = new List<Animal> { swiperAnimal }.AsQueryable();
            SetUpReturningSwiperAnimal(repositoryMock, queryable);
            SetUpReturningSwipeeAnimal(repositoryMock, swipeeAnimal);
            SetUpReturningUser(repositoryMock);
            repositoryMock.Setup(r => r.SaveChangesAsync()).
                Throws(new Exception("Simulating save failure"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to swipe on the animal", result.ErrorMessage);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        private static void SetUpReturningSwiperAnimal(
           Mock<IRepository> repositoryMock,
           IQueryable<Animal> queryable)
        {
            var asyncEnumerable =
                new TestAsyncEnumerableEfCore<Animal>(queryable);

            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<Animal, bool>>>())).
                Returns(asyncEnumerable);
        }

        private static void SetUpReturningSwipeeAnimal(
           Mock<IRepository> repositoryMock,
           Animal swipeeAnimal)
        {
            repositoryMock.
                Setup(r => r.
                FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>())).
                ReturnsAsync(swipeeAnimal);
        }

        private static void SetUpReturningUser(
          Mock<IRepository> repositoryMock)
        {
            repositoryMock.
                Setup(r => r.
                AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync(true);
        }
    }
}
