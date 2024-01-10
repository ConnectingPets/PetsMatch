namespace Tests.Animal
{
    using System.Linq.Expressions;

    using Moq;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Animal.ShowAnimalToEdit;

    [TestFixture]
    public class ShowAnimalToEditTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowAnimalToEditQueryHandler handler;
        private Animal animal;
        private ShowAnimalToEditQuery query;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new ShowAnimalToEditQueryHandler(repositoryMock.Object);
            animal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test",
                OwnerId = Guid.Parse("8176d75f-13e9-4f1d-8b15-8364fa886a37"),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                         Id = "IdPhoto",
                         IsMain = true,
                         Url = "URL"
                    }
                },
                Breed = new Breed()
                {
                    BreedId = 1,
                    Name = "BreedTest",
                    CategoryId = 1
                }
            };
            query = new ShowAnimalToEditQuery()
            {
                AnimalId = "af1da119-7c50-4145-ba3a-3ba72d53e574",
                UserId = "8176d75f-13e9-4f1d-8b15-8364fa886a37"
            };
        }


        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<Animal, bool>>>()))
                .Returns(MockDbSet(animal));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Data.Name, animal.Name);
            Assert.AreEqual(result.Data.BreedName, animal.Breed.Name);
            Assert.AreEqual(result.Data.Photos.Count(), 1);
        }


        [Test]
        public async Task Handle_InvalidAnimalId_ReturnsFailureResult()
        {
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_NotRightUser_ReturnsFailureResult()
        {
            query.UserId = "a48b6b31-cf50-4676-b244-223a6f691cdc";
            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<Animal, bool>>>()))
                .Returns(MockDbSet(animal));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you!", result.ErrorMessage);
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
