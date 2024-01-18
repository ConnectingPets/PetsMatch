namespace Tests.Marketplace
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Marketplace.GetAnimal;

    [TestFixture]
    public class GetAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private GetAnimalQueryHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new GetAnimalQueryHandler(repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            var animal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test",
                OwnerId = Guid.NewGuid(),
                Breed = new Breed()
                {
                    Name = "TestBreed",
                    CategoryId = 2
                },
                Photos = new List<Photo>
                {
                     new Photo()
                     {
                          Id = "Id",
                          IsMain = true,
                          Url = "URL"
                     }
                },
                Owner = new User()
                {
                    Name = "Test"
                }
            };

            var queryable =
                    new List<Animal> { animal }.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.
                Handle(new GetAnimalQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(animal.Name, result.Data.Name);
            Assert.AreEqual(animal.Photos.First().Url, result.Data.Photos.First().Url);
            Assert.AreEqual(animal.Owner.Name, result.Data.UserName);
        }

        [Test]
        public async Task Handle_InvalidCommand_AnimalNotFound_ReturnsFailureResult()
        {
            var queryable =
                    new List<Animal> {}.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.
                Handle(new GetAnimalQuery(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        private static void SetUpReturningAnimals(
             IQueryable<Animal> queryable,
             Mock<IRepository> repositoryMock)
        {
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<Animal>(queryable);
            repositoryMock.
                Setup(r => r.
                All(It.IsAny<Expression<Func<Animal, bool>>>())).
                Returns(asyncEnumerable);
        }
    }
}
