namespace Tests.User
{
    using System.Linq.Expressions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;

    using Domain;
    using Application.DTOs.User;
    using Persistence.Repositories;

    using static Application.User.EditUser;

    public class EditUserTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<UserManager<User>> userManagerMock;
        private EditUserHandler handler;
        private EditUserCommand command;
        private User user;

        [SetUp]
        public void Setup()
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
            handler = new EditUserHandler(
                repositoryMock.Object, userManagerMock.Object);
            command = new EditUserCommand()
            {
                User = new EditUserDto()
                {
                    Roles = ["Matching"],
                    Name = "New name",
                    Description = "New Description"
                }
            };
            user = new User()
            {
                Name = "Test"
            };
        }

        [Test]
        public async Task Handle_ValidCommand_EditUser()
        {
            SetUpReturningUser(repositoryMock, user);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"The user {command.User.Name} is edited successfully", result.SuccessMessage);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenUserIsNotFound()
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenRepositoryThrowsError()
        {
            SetUpReturningUser(repositoryMock, user);
            repositoryMock.
                Setup(r => r.SaveChangesAsync()).
                ThrowsAsync(new Exception());
            
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to edit the user {user.Name}", result.ErrorMessage);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        private static void SetUpReturningUser(
            Mock<IRepository> repositoryMock,
            User user)
        {
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync(user);
        }
    }
}
