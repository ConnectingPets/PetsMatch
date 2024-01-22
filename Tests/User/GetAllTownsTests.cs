namespace Tests.User
{
    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;

    using static Application.User.GetAllTowns;

    [TestFixture]
    public class GetAllTownsTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            var repositoryMock = new Mock<IRepository>();
            var handler = new GetAllTownsQueryHandler(repositoryMock.Object);

            var userOne = new User()
            {
                Name = "Test",
                City = "Plovdiv"
            };
            var userTwo = new User()
            {
                Name = "Test",
                City = "Sofia"
            };
            var userThree = new User()
            {
                Name = "Test",
                City = "Sofia"
            };

            var users = 
                new List<User>() { userOne, userTwo, userThree }.AsQueryable();
            var asyncEnumerable =
                 new TestAsyncEnumerableEfCore<User>(users);
            repositoryMock.
                Setup(r => r.AllReadonly<User>()).Returns(asyncEnumerable);

            var result = await handler.Handle(new GetAllTownsQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual(userOne.City, result.Data.First());
            Assert.AreEqual(userTwo.City, result.Data.Skip(1).First());
        }
    }
}
