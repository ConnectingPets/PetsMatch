namespace Tests.Marketplace
{
    using System.Linq;
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Marketplace.AllAnimalsForSale;

    public class AllAnimalsForSaleTests
    {
        private Mock<IRepository> repositoryMock;
        private AllAnimalsForSaleQueryHandler handler;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler =
                new AllAnimalsForSaleQueryHandler(repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShowAllAnimalForSale_ReturnsSuccessResult()
        {
            var animalOne = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test1",
                Photos = new List<Photo>()
                {
                      new Photo()
                      {
                           Id = "ID",
                           IsMain = true,
                           Url = "URL"
                      }
                },
                Breed = new Breed()
                {
                    CategoryId = 2,
                    Name = "BreedTest",
                    Category = new AnimalCategory()
                    {
                        Name = "AnimalCategoryTest"
                    }
                },
                OwnerId = Guid.NewGuid(),
                Owner = new User() 
                {
                    Name = "TestOwner"
                }
            };
            var animalTwo = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test2",
                Photos = new List<Photo>()
                {
                      new Photo()
                      {
                           Id = "ID",
                           IsMain = true,
                           Url = "URL"
                      }
                },
                Breed = new Breed()
                {
                    CategoryId = 2,
                    Name = "BreedTest",
                    Category = new AnimalCategory()
                    {
                        Name = "AnimalCategoryTest"
                    }
                },
                OwnerId = Guid.NewGuid(),
                Owner = new User()
                {
                    Name = "TestOwner"
                }
            };

            var queryable =
                new List<Animal> { animalOne, animalTwo }.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.Handle(new AllAnimalsForSaleQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual("Test1", result.Data.First().Name);
            Assert.AreEqual("Test2", result.Data.Skip(1).First().Name);
        }

        [Test]
        public async Task Handle_ShowAllAnimalForSale_WhenNoAnimalsFound_ReturnsFailureResult()
        {
            var queryable =
               new List<Animal> { }.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await handler.Handle(new AllAnimalsForSaleQuery(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("We still don't have animals for sale", result.ErrorMessage);
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
