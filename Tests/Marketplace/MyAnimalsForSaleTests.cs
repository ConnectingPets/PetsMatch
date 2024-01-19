namespace Tests.Marketplace
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Marketplace.MyAnimalsForSale;

    [TestFixture]
    public class MyAnimalsForSaleTests
    {
        private Mock<IRepository> repositoryMock;
        private MyAnimalForSaleQueryHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new MyAnimalForSaleQueryHandler(repositoryMock.Object);
        }

        [Test]
        public async Task Handle_UserWithPets_ReturnsSuccessResult()
        {
            var animalOne = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test1",
                OwnerId = Guid.NewGuid(),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                        Id = "Id",
                        IsMain = true,
                        Url = "URL"
                    }
                }
            };
            var animalTwo = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test2",
                OwnerId = Guid.NewGuid(),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                        Id = "Id",
                        IsMain = true,
                        Url = "URL"
                    }
                }
            };

            var queryable =
                    new List<Animal> { animalOne, animalTwo}.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.Handle(new MyAnimalsForSaleQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual(animalOne.Name, result.Data.First().Name);
            Assert.AreEqual(animalTwo.Name, result.Data.Skip(1).First().Name);
        }

        [Test]
        public async Task Handle_UserWithoutPets_ReturnsFailureResult()
        {
            var queryable =
                    new List<Animal> {}.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.Handle(new MyAnimalsForSaleQuery(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You still don't have animal for sale", result.ErrorMessage);
        }

        private static void SetUpReturningAnimals(
            IQueryable<Animal> queryable,
            Mock<IRepository> repositoryMock)
        {
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<Animal>(queryable);
            repositoryMock.
                Setup(r => r.
                AllReadonly(It.IsAny<Expression<Func<Animal, bool>>>())).
                Returns(asyncEnumerable);
        }
    }
}


