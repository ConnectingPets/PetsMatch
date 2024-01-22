namespace Tests.User
{
    using System.Linq.Expressions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Moq;
    using Domain;
    using Persistence.Repositories;

    using static Application.User.DeleteRole;

    [TestFixture]
    public class DeleteRoleTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<UserManager<User>> userManagerMock;
        private DeleteRoleHandler handler;
        private User user;
        private List<string> roles;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
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
            handler = new DeleteRoleHandler(
                repositoryMock.Object,
                userManagerMock.Object);
            user = new User()
            {
                Name = "Test user"
            };
            roles = new List<string>() { "Matching", "Marketplace" };
        }

        [Test]
        public async Task Handle_ValidCommand_DeleteRole()
        {
            var command = new DeleteRoleCommand()
            {
                RoleName = "Matching"
            };

            SetUpReturningUser(repositoryMock, user);
            SetUpReturningRoles(userManagerMock, roles);

            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("The profile Matching is deleted successfully", result.SuccessMessage);
            userManagerMock.
                Verify(um => um.RemoveFromRoleAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>()),
                    Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenUserIsNotFound()
        {
            var result = await handler.
                Handle(new DeleteRoleCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenUserHaveOneRole()
        {
            var roles = new List<string>() {"Matching"};
            var command = new DeleteRoleCommand()
            {
                RoleName = "Matching"
            };

            SetUpReturningUser(repositoryMock, user);
            SetUpReturningRoles(userManagerMock, roles);

            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to delete Matching profile", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_FailedToDeleteRole_ReturnsFailureResult()
        {
            var command = new DeleteRoleCommand()
            {
                RoleName = "Matching"
            };

            SetUpReturningUser(repositoryMock, user);
            SetUpReturningRoles(userManagerMock, roles);
            userManagerMock.Setup(um => um.
            RemoveFromRoleAsync(
                It.IsAny<User>(),
                It.IsAny<string>())).
                ThrowsAsync(new Exception());

            var result = await handler.
                Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to delete Matching profile", result.ErrorMessage);
        }

        private static void SetUpReturningUser(
            Mock<IRepository> repositoryMock,
            User user)
        {
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync(user);
        }

        private static void SetUpReturningRoles(
            Mock<UserManager<User>> userManagerMock,
            List<string> roles)
        {
            userManagerMock.
                Setup(um => um.GetRolesAsync(It.IsAny<User>())).
                ReturnsAsync(roles);
        }
    }
}
