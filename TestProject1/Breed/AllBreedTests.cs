namespace Tests.Breed
{
    using Moq;
    using Persistence.Repositories;
    using static Application.Breed.AllBreeds;

    [TestFixture]
    public class AllBreedTests
    {
        private Mock<IRepository> repositoryMock;
        private AllBreedsQueryHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AllBreedsQueryHandler(repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShoudReturn_AllBreeds()
        {

        }
    }
}
