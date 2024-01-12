namespace Tests.Photo
{
    using System.Linq.Expressions;

    using Moq;
    using MediatR;

    using Domain;
    using Application.Response;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.Photo.DeleteAnimalPhoto;

    [TestFixture]
    public class DeleteAnimalPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private DeleteAnimalPhotoHandler handler;
        private Photo photo;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new DeleteAnimalPhotoHandler(photoServiceMock.Object, repositoryMock.Object);
            photo = new Photo()
            {
                Id = "Id",
                IsMain = false,
                Url = "URL",
            };
        }

        [Test]
        public async Task Handle_ShouldDeletePhoto_WhenGivingCorrectData()
        {
            SetUpReturningPhoto(repositoryMock,photo);
            photoServiceMock.Setup(ps => ps.DeleteAnimalPhotoAsync(photo))
                .ReturnsAsync(Result<Unit>.Success(Unit.Value, "Successfully delete photo"));

            var result = await handler.Handle(new DeleteAnimalPhotoCommand(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Successfully delete photo", result.SuccessMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenPhotoDoesNotExist()
        {
            var result = await handler.Handle(new DeleteAnimalPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This photo does not exist! Please select existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenPhotoIsMain()
        {
            photo.IsMain = true;
            SetUpReturningPhoto(repositoryMock, photo);

            var result = await handler.Handle(new DeleteAnimalPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This is your main photo! You can not delete it", result.ErrorMessage);
        }

        private static void SetUpReturningPhoto(
            Mock<IRepository> repositoryMock,
            Photo photo)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync(It.IsAny<Expression<Func<Photo, bool>>>())).ReturnsAsync(photo);
        }
    }
}
