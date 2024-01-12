namespace Tests.Breed
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;

    using static Application.Breed.AllBreeds;

    [TestFixture]
    public class AllBreedTests
    {
        private Mock<IRepository> repositoryMock;
        private AllBreedsQueryHandler handler;
        private AllBreedsQuery command;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AllBreedsQueryHandler(repositoryMock.Object);
            command = new AllBreedsQuery()
            {
                CategoryId = 1,
            };
        }

        [Test]
        public async Task Handle_ShouldReturn_AllBreeds()
        {
            var animalCategory = new AnimalCategory()
            {
                AnimalCategoryId = 1,
                Name = "Animal category test",
                Breeds = new List<Breed>()
                {
                    new Breed()
                    {
                          CategoryId = 1,
                          Name = "Breed test 1"
                    },
                    new Breed()
                    {
                        CategoryId = 1,
                          Name = "Breed test 2"
                    }
                }
            };

            var queryable =
               new List<AnimalCategory> { animalCategory }.AsQueryable();
            SetUpReturningCategory(repositoryMock, queryable);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Data.Count(), 2);
            Assert.AreEqual("Breed test 1", result.Data.First().Name);
            Assert.AreEqual("Breed test 2", result.Data.Skip(1).First().Name);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenCategoryNotExist()
        {
            var queryable =
               new List<AnimalCategory> { }.AsQueryable();
            SetUpReturningCategory(repositoryMock, queryable);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This category does not exist. Please select existing one", result.ErrorMessage);
        }

        private static void SetUpReturningCategory(
            Mock<IRepository> repositoryMock,
            IQueryable<AnimalCategory> queryable)
        {
            var asyncEnumerable =
                 new TestAsyncEnumerableEfCore<AnimalCategory>(queryable);
            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<AnimalCategory, bool>>>()))
                .Returns(asyncEnumerable);
        }
    }
}
