namespace Tests.Photo
{
    using System.Linq.Expressions;

    using Moq;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Domain;
    using Application.Response;
    using Persistence.Repositories;
    using Application.Service.Interfaces;
    
    using static Application.Photo.AddUserPhoto;

    [TestFixture]
    public class AddUserPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private AddUserPhotoCommandHandler handler;
        private User user;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new AddUserPhotoCommandHandler(photoServiceMock.Object, repositoryMock.Object);
            user = new User()
            {
                Name = "Test",
            };
        }

        [Test]
        public async Task Handler_ShouldAddUserPhoto_WhenGivingCorrectData()
        {
            var command = SetUpReturningPhoto();
            SetUpReturningUser(repositoryMock, user);

            photoServiceMock.Setup(ps => ps.
            AddUserPhotoAsync(command.File, command.UserId)).
            ReturnsAsync(Result<Unit>.
            Success(Unit.Value, "Successfully upload photo"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Successfully upload photo", result.SuccessMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenUserHavePhoto()
        {
            user.PhotoId = "Id";

            var command = SetUpReturningPhoto();
            SetUpReturningUser(repositoryMock, user);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You already have photo!", result.ErrorMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotosIsNull()
        {
            SetUpReturningUser(repositoryMock, user);
            var result = await handler.
                Handle(new AddUserPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("File is not selected or empty", result.ErrorMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotosArrayIsEmpty()
        {
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(0);

            var command = new AddUserPhotoCommand()
            {
                File = formFileMock.Object
            };

            SetUpReturningUser(repositoryMock, user);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("File is not selected or empty", result.ErrorMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotoDoNotStartWithImage()
        {
            SetUpReturningUser(repositoryMock, user);

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            formFileMock.Setup(f => f.ContentType).Returns("not image");

            var command = new AddUserPhotoCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                File = formFileMock.Object
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This file is not an image", result.ErrorMessage);
        }

        private static AddUserPhotoCommand SetUpReturningPhoto()
        {
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            formFileMock.Setup(f => f.ContentType).Returns("image");

            var command = new AddUserPhotoCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                File = formFileMock.Object
            };

            return command;
        }

        private static void SetUpReturningUser(
            Mock<IRepository> repositoryMock,
            User user)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).
                 ReturnsAsync(user);
        }
    }
}
