namespace Tests.Photo
{
    using System.Linq.Expressions;

    using Moq;
    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Service.Interfaces;

    using static Application.Photo.SetAnimaMainPhoto;

    [TestFixture]
    public class SetAnimalMainTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private SetAnimalMainPhotoCommandHandler handler;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new SetAnimalMainPhotoCommandHandler(photoServiceMock.Object, repositoryMock.Object);
        }
        [Test]
        public async Task Handler_ShouldSetMainPhoto_WhenGivingCorrectData()
        {
            var photo = new Photo()
            {
                Id = "Id",
                IsMain = true,
                Url = "URL",
            };

            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync(It.IsAny<Expression<Func<Photo,bool>>>())).
                ReturnsAsync(photo);

            photoServiceMock.Setup(ps => ps.SetAnimalMainPhotoAsync(photo)).
                ReturnsAsync(Result<Unit>.
                Success(Unit.Value, "You successfully set main photo"));

            var result = await handler.
                Handle(new SetAnimalMainPhotoCommand(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("You successfully set main photo", result.SuccessMessage);
        }

        [Test]
        public async Task Handler_ShouldReturnError_WhenPhotoDoesNotExist()
        {
            var result = await handler.
                Handle(new SetAnimalMainPhotoCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This photo does not exist! Please select existing one", result.ErrorMessage);
        }
    }
}
