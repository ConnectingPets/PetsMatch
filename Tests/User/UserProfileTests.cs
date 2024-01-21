namespace Tests.User
{
    using System.Linq.Expressions;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.User.UserProfile;

    [TestFixture]
    public class UserProfileTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<ITokenService> tokenServiceMock;
        private Mock<UserManager<User>> userManagerMock;
        private UserProfileHandler handler;


        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            tokenServiceMock = new Mock<ITokenService>();
            userManagerMock = new Mock<UserManager<User>>(
                 new Mock<IUserStore<User>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<IPasswordHasher<User>>().Object,
                 new IUserValidator<User>[0],
                 new IPasswordValidator<User>[0],
                 new Mock<ILookupNormalizer>().Object,
                 new Mock<IdentityErrorDescriber>().Object,
                 new Mock<IServiceProvider>().Object,
                 new Mock<ILogger<UserManager<User>>>().Object
             );
            handler = new UserProfileHandler(repositoryMock.Object, tokenServiceMock.Object, userManagerMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsUserProfile()
        {
            var user = new User()
            {
                Name = "Test",
            };
            var queryable = new List<User> { user }.AsQueryable();
           SetUpReturningUser(repositoryMock, queryable);

            var result = await  handler.Handle(new UserProfileQuery(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(user.Name, result.Data.Name);
        }

        [Test]
        public async Task Handle_InvalidUserId_ReturnsErrorResult()
        {
            var queryable = new List<User> { }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);

            var result = await handler.Handle(new UserProfileQuery(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        private static void SetUpReturningUser(
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
