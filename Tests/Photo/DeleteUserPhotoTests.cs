namespace Tests.Photo
{
    using System.Linq.Expressions;

    using Moq;
    using MediatR;

    using Domain;
    using Application.Response;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.Photo.DeleteUserPhoto;

    [TestFixture]
    public class DeleteUserPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private DeleteUserPhotoCommandHandler handler;
        private Photo photo;
        private User user;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new DeleteUserPhotoCommandHandler(photoServiceMock.Object, repositoryMock.Object);
            photo = new Photo()
            {
                Id = "Id",
                IsMain = false,
                Url = "URL",
            };
            user = new User()
            {
                Name = "Test",
                Id = Guid.NewGuid(),
                PhotoId = "photoId"
            };
        }

        [Test]
        public async Task Handle_ShouldDeletePhoto_WhenGivingCorrectData()
        {
            SetUpReturningPhoto(repositoryMock, photo);
            SetUpReturningUser(repositoryMock, user);

            var command = new DeleteUserPhotoCommand()
            {
                UserId = Guid.NewGuid().ToString()
            };

            photoServiceMock.Setup(ps => ps.
            DeleteUserPhotoAsync(photo, command.UserId)).
                ReturnsAsync(Result<Unit>.Success(Unit.Value, "Successfully delete photo"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Successfully delete photo", result.SuccessMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenPhotoDoesNotExist()
        {
            SetUpReturningUser(repositoryMock, user);
            var result = await handler.Handle(new DeleteUserPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This photo does not exist! Please select existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenPhotoIsMain()
        {
            user.PhotoId = null;
            SetUpReturningUser(repositoryMock, user);

            var result = await handler.Handle(new DeleteUserPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You don't have photo yet!", result.ErrorMessage);
        }

        private static void SetUpReturningPhoto(
           Mock<IRepository> repositoryMock,
           Photo photo)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync(It.IsAny<Expression<Func<Photo, bool>>>())).ReturnsAsync(photo);
        }

        private static void SetUpReturningUser(
          Mock<IRepository> repositoryMock,
          User user)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
        }
    }
}
