namespace Tests.Animal
{
    using System.Linq;
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Animal.AllAnimal;

    [TestFixture]
    public class AllAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private AllAnimalQueryHandler handler;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AllAnimalQueryHandler(repositoryMock.Object);

        }

        [Test]
        public async Task Handle_UserWithPets_ReturnsSuccessResult()
        {
            var query = new AllAnimalQuery
            {
                OwnerId = "F6E0FC1A-7726-4519-A599-0114A1EB1875"
            };

            var userWithPets = new User
            {
                Id = Guid.Parse("F6E0FC1A-7726-4519-A599-0114A1EB1875"),
                Name = "Test",
                Animals = new List<Animal>
                {
                    new Animal
                    {
                        AnimalId =
                        Guid.Parse("aa207b22-7865-42d8-83e0-dcf72376f4bd"),      Name = "Pet1",
                        Age = 1,
                        AnimalStatus = AnimalStatus.ForSwiping,
                        BreedId = 2,
                        Gender = Gender.Male,
                        HealthStatus = HealthStatus.Vaccinated,
                        IsEducated = true,
                        IsHavingValidDocuments = true,
                        Photos = new List<Photo>()
                        {
                            new Photo()
                           {
                                Id = "1",
                                IsMain = true,
                                Url = "TestUrl"
                           }
                        },
                        OwnerId =
                        Guid.Parse("F6E0FC1A-7726-4519-A599-0114A1EB1875")
                    },
                    new Animal
                    {
                        AnimalId =
                        Guid.Parse("ab661c5c-3e89-4beb-8b98-923fd08c3935"),       Name = "Pet2",
                        Age = 1,
                        AnimalStatus = AnimalStatus.ForSwiping,
                        BreedId = 2,
                        Gender = Gender.Male,
                        HealthStatus = HealthStatus.Vaccinated,
                        IsEducated = true,
                        IsHavingValidDocuments = true,
                        Photos = new List<Photo>()
                        {
                           new Photo()
                           {
                                Id = "1",
                                IsMain = true,
                                Url = "TestUrl"
                           }
                        },
                        OwnerId =
                        Guid.Parse("F6E0FC1A-7726-4519-A599-0114A1EB1875")
                    }
                }
            };

            var queryable = new List<User> { userWithPets }.AsQueryable();
            SetUpAllAnimals(repositoryMock, queryable);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual("Pet1", result.Data.First().Name);
            Assert.AreEqual("Pet2", result.Data.Skip(1).First().Name);
        }

        [Test]
        public async Task Handle_UserWithoutPets_ReturnsFailureResult()
        {
            var query = new AllAnimalQuery
            {
                OwnerId = "F6E0FC1A-7726-4519-A599-0114A1EB1875"
            };

            var userWithoutPets = new User()
            {
                Id = Guid.Parse("F6E0FC1A-7726-4519-A599-0114A1EB1875"),
                Name = "Test",
                Animals = new List<Animal>()
            };

            var queryable = new List<User> { userWithoutPets }.AsQueryable();
            SetUpAllAnimals(repositoryMock, queryable);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You don't have pets yet", result.ErrorMessage);
        }

        private static void SetUpAllAnimals(
            Mock<IRepository> repositoryMock,
            IQueryable<User> queryable)
        {
            var asyncEnumerable =
                new TestAsyncEnumerableEfCore<User>(queryable);

            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<User, bool>>>())).
                Returns(asyncEnumerable);
        }
    }
}
