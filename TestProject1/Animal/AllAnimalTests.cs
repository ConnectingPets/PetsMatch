namespace Tests.Animal
{
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;
    using Moq;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Animal.AllAnimal;

    [TestFixture]
    public class AllAnimalTests
    {
        private Mock<IRepository> repositoryMock;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();

        }
        [Test]
        public async Task Handle_UserWithPets_ReturnsSuccessResult()
        {
            var handler = new AllAnimalQueryHandler(repositoryMock.Object);

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

            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(MockDbSet(userWithPets));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual("Pet1", result.Data.First().Name);
            Assert.AreEqual("Pet2", result.Data.Skip(1).First().Name);
        }

        [Test]
        public async Task Handle_UserWithoutPets_ReturnsFailureResult()
        {
            var handler = new AllAnimalQueryHandler(repositoryMock.Object);

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

            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<User, bool>>>())).Returns(MockDbSet(userWithoutPets));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You don't have pets yet", result.ErrorMessage);
        }

        private static DbSet<T> MockDbSet<T>(T elements) where T : class
        {
            var queryable = new List<T> { elements }.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSetMock.Object;
        }

    }
}
