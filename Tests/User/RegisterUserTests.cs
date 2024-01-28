namespace Tests.User
{
    using Moq;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using Application.Service.Interfaces;

    using static Application.User.RegisterUser;

    [TestFixture]
    public class RegisterUserTests
    {
        private Mock<ITokenService> tokenServiceMock;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<SignInManager<User>> userSingInManagerMock;
        private RegisterUserHandler handler;

        [SetUp]
        public void SetUp()
        {
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

            userSingInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            handler = new RegisterUserHandler(
                userManagerMock.Object,
                userSingInManagerMock.Object,
                tokenServiceMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_RegistersUser()
        {
            var command = new RegisterUserCommand()
            {
                Roles = new List<string>()
                {
                    "Matching"
                }.ToArray(),
                Name = "Test name"
            };

            userManagerMock.
                Setup(um => um.
                CreateAsync(It.IsAny<User>(), It.IsAny<string>())).
                ReturnsAsync(IdentityResult.Success);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(command.Name, result.Data.Name);
        }

        [Test]
        public async Task Handle_InvalidCommand_WhenUserManagerThrowsError()
        {
            var identityError = new IdentityError
            {
                Code = "ErrorCode",
                Description = "Error description"
            };

            userManagerMock.
                Setup(um => um.
                CreateAsync(It.IsAny<User>(),
                It.IsAny<string>())).
                ReturnsAsync(IdentityResult.Failed(identityError));

            var result = await handler.
                Handle(new RegisterUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("User registration failed", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_InvalidCommand_WhenSignInManagerThrowsError()
        {
            userSingInManagerMock.
                Setup(sm => sm.
                SignInAsync(It.IsAny<User>(),
                It.IsAny<bool>(),
                It.IsAny<string>())).
                Throws(new Exception());

            var result = await handler.
                Handle(new RegisterUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("User registration failed", result.ErrorMessage);
        }
    }
}
