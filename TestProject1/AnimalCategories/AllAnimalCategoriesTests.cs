namespace Tests.AnimalCategories
{
    using Domain;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq;
    using static Application.AnimalCategory.AllAnimalCategories;

    [TestFixture]
    public class AllAnimalCategoriesTests
    {
        [Test]
        public async Task Handle_ShouldReturn_AllAnimalCategiries()
        {
            AnimalCategory animalCategory = new AnimalCategory()
            {
                Name = "Test",
            };
            var queryable = 
                new List<AnimalCategory> { animalCategory }.AsQueryable();
            var asyncEnumerable =
                new TestAsyncEnumerableEfCore<AnimalCategory>(queryable);

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(r => r.AllReadonly<AnimalCategory>())
                .Returns(asyncEnumerable);

            var handler = 
                new AllAnimalCategoriesQueryHandler(mockRepository.Object);
            var result = await handler.
                Handle(new AllAnimalCategoriesQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Data.Count(), 1);
            Assert.AreEqual(result.Data.First().Name, "Test");

        }
    }
}
