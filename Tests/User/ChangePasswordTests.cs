namespace Tests.User
{
    using Moq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Domain;
    using Application.DTOs.User;

    using static Application.User.ChangePassword;

    [TestFixture]
    public class ChangePasswordTests
    {
        private Mock<UserManager<User>> userManagerMock;
        private ChangePasswordCommandHandler handler;
        private ChangePasswordCommand command;

        [SetUp]
        public void SetUp()
        {
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
            handler = new ChangePasswordCommandHandler(userManagerMock.Object);
            command = new ChangePasswordCommand()
            {
                User = new User()
                {
                    Name = "Test",
                },
                Dto = new ChangePasswordDto()
            };
        }

        [Test]
        public async Task Handle_ChangePassword_WhenGivingCorrectData()
        {
            SetUpCheckingPassword(userManagerMock);
            SetUpChangingPassword(userManagerMock, IdentityResult.Success);

            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("You successfully changed your password", result.SuccessMessage);
            userManagerMock.Verify(um => um.ChangePasswordAsync(
                It.IsAny<User>(), 
                It.IsAny<string>(), 
                It.IsAny<string>()), 
                Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenOldPasswordIsNotCorrect()
        {
            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The old password is incorrect", result.ErrorMessage);
        }

        [Test]
        public async Task 
            Handle_ReturnsError_WhenUserManager_FailToChangePassword()
        {
            var identityError = new IdentityError
            {
                Code = "ErrorCode",
                Description = "Error description"
            };

            SetUpCheckingPassword(userManagerMock);
            SetUpChangingPassword(userManagerMock, 
                IdentityResult.Failed(identityError));

            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Changing password failed", result.ErrorMessage);
        }

        private static void SetUpCheckingPassword(
            Mock<UserManager<User>> userManagerMock)
        {
            userManagerMock.
                Setup(um => um.CheckPasswordAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>())).
                    ReturnsAsync(true);
        }

        private static void SetUpChangingPassword(
            Mock<UserManager<User>> userManagerMock,
            IdentityResult result)
        {
            userManagerMock.
                Setup(um => um.ChangePasswordAsync(
                    It.IsAny<User>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>())).
                    ReturnsAsync(result);
        }
    }
}
