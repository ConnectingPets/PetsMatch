namespace Tests.Marketplace
{
    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Marketplace.ShowAnimalMarketplaceToEdit;

    [TestFixture]
    public class ShowAnimalMarketplaceToEditTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowAnimalMarketplaceToEditQueryHandler hander;
        private Animal animal;
        private ShowAnimalMarketplaceToEditQuery command;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            animal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForAdoption,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test",
                OwnerId = Guid.Parse("a275549d-1b57-4de8-9b64-dc8b8bcf0789"),
                Breed = new Breed()
                {
                    CategoryId = 1,
                    Name = "Test Breed",
                },
                Photos = new List<Photo>()
                {
                     new Photo()
                     {
                          Id = "Id",
                          IsMain = true,
                          Url = "Url"
                     }
                }
            };
            hander =
                new ShowAnimalMarketplaceToEditQueryHandler(repositoryMock.Object);
            command = new ShowAnimalMarketplaceToEditQuery()
            {
                AnimalId = Guid.NewGuid().ToString(),
                UserId = "a275549d-1b57-4de8-9b64-dc8b8bcf0789"
            };
        }

        [Test]
        public async Task 
            Handle_ShowAnimalMarketplaceToEdit_ReturnsSuccessResult()
        {
            var queryable = new List<Animal> { animal }.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await hander.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Test", result.Data.Name);
            Assert.AreEqual("Test Breed", result.Data.BreedName);
        }

        [Test]
        public async Task 
            Handle_ReturnsErrorResult_WhenAnimalIsNotFound()
        {
            var queryable = new List<Animal> {}.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await hander.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task
            Handle_ReturnsErrorResult_WhenUserIsNotOwner()
        {
            animal.OwnerId = Guid.NewGuid();
            var queryable = new List<Animal> { animal }.AsQueryable();
            SetUpReturningAnimals(queryable, repositoryMock);

            var result = await hander.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you!", result.ErrorMessage);
        }

        private static void SetUpReturningAnimals(
             IQueryable<Animal> queryable,
             Mock<IRepository> repositoryMock)
        {
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<Animal>(queryable);
            repositoryMock.
                Setup(r => r.
                All<Animal>()).
                Returns(asyncEnumerable);
        }
    }
}
