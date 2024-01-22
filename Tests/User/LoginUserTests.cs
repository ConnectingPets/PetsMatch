namespace Tests.User
{
    using System.Linq.Expressions;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.User;
    using Application.Service.Interfaces;

    using static Application.User.LoginUser;

    [TestFixture]
    public class LoginUserTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<ITokenService> tokenServiceMock;
        private Mock<SignInManager<User>> signInManagerMock;
        private LoginUserHandler handler;
        private User user;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            tokenServiceMock = new Mock<ITokenService>();

            var userManagerMock = new Mock<UserManager<User>>(
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
            signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            handler = new LoginUserHandler(
                signInManagerMock.Object,
                repositoryMock.Object,
                tokenServiceMock.Object);
            user = new User()
            {
                Name = "Test",
            };
        }

        [Test]
        public async Task Handle_ValidCommand_LoginUser()
        {
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            SetUpSignInManager(signInManagerMock, SignInResult.Success);

            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(user.Name, result.Data.Name);
            VerifySignInManager(user, result, signInManagerMock);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenUserIsNotFound()
        {
            var queryable = new List<User> { }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);

            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenSignInManager_ReturnsError()
        {
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            SetUpSignInManager(signInManagerMock, SignInResult.Failed);

            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid email or password", result.ErrorMessage);
            VerifySignInManager(user, result, signInManagerMock);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenSignInManager_ThrowsError()
        {
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            signInManagerMock.
                Setup(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>())).
                    ThrowsAsync(new Exception());

            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid email or password", result.ErrorMessage);
            VerifySignInManager(user, result, signInManagerMock);
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

        private static void SetUpSignInManager(
            Mock<SignInManager<User>> signInManagerMock,
            SignInResult result)
        {
            signInManagerMock.
                Setup(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>())).
                    ReturnsAsync(result);
        }

        private static void VerifySignInManager(
            User user,
            Result<UserDto> result,
            Mock<SignInManager<User>> signInManagerMock)
        {
            signInManagerMock.
                Verify(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                    Times.Once());
        }
    }
}
