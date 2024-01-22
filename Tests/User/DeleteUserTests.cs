namespace Tests.User
{
    using System.Linq;
    using System.Linq.Expressions;

    using MediatR;
    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Service.Interfaces;

    using static Application.User.DeleteUser;

    [TestFixture]
    public class DeleteUserTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private DeleteUserHandler handler;
        private User user;
        private Result<Unit> successResult;
        private Result<Unit> failureResult;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
            handler = new DeleteUserHandler(
                repositoryMock.Object,
                photoServiceMock.Object);
            user = new User()
            {
                Name = "Test",
                Animals = new List<Animal>()
                {
                    new Animal()
                    {
                        Age = 1,
                        AnimalStatus = AnimalStatus.ForSwiping,
                        BreedId = 1,
                        Gender = Gender.Female,
                        HealthStatus = HealthStatus.Dewormed,
                        IsEducated = true,
                        IsHavingValidDocuments = true,
                        Name = "Test",
                        OwnerId = Guid.NewGuid(),
                        Photos = new List<Photo>()
                        {
                            new Photo()
                            {
                                Id = "Id",
                                IsMain = true,
                                Url = "Url"
                            }
                        },
                        AnimalMatches = new List<AnimalMatch>()
                        {
                            new AnimalMatch()
                            {
                                 AnimalId = Guid.NewGuid(),
                                 MatchId = Guid.NewGuid(),
                            }
                        }
                    }
                },
                Photo = new Photo()
                {
                    Id = "Id",
                    IsMain = true,
                    Url = "Url"
                }
            };
            successResult = Result<Unit>.
                Success(Unit.Value, "Successfully delete photo");
            failureResult = Result<Unit>.Failure("Failed to delete photo");
        }

        [Test]
        public async Task Handle_ValidCommand_DeleteUser()
        {
            var queryable = new List<User>() { user }.AsQueryable();

            SetUpReturningUser(repositoryMock, queryable);
            SetUpDeleteAnimalPhoto(photoServiceMock, successResult);
            SetUpDeleteUserPhoto(photoServiceMock, successResult);

            var result = await handler.
                Handle(new DeleteUserCommand(), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"The user {user.Name} is deleted successfully", result.SuccessMessage);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenUserNotExist()
        {
            var queryable = new List<User>() {}.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);

            var result = await handler.
               Handle(new DeleteUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenDeleteAllAnimalPhotos_ReturnsError()
        {
            var queryable = new List<User>() { user }.AsQueryable();

            SetUpReturningUser(repositoryMock, queryable);
            SetUpDeleteAnimalPhoto(photoServiceMock, failureResult);

            var result = await handler.
               Handle(new DeleteUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to delete photo", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenDeleteUserPhotos_ReturnsError()
        {
            var queryable = new List<User>() { user }.AsQueryable();

            SetUpReturningUser(repositoryMock, queryable);
            SetUpDeleteAnimalPhoto(photoServiceMock, successResult);
            SetUpDeleteUserPhoto(photoServiceMock, failureResult);

            var result = await handler.
               Handle(new DeleteUserCommand(), CancellationToken.None);

           Assert.IsFalse(result.IsSuccess);
           Assert.AreEqual($"Failed to delete {user.Name}'s photo", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_FailedToDeleteUser_ReturnsFailureResult()
        {
            var queryable = new List<User>() { user }.AsQueryable();

            SetUpReturningUser(repositoryMock, queryable);
            SetUpDeleteAnimalPhoto(photoServiceMock, successResult);
            SetUpDeleteUserPhoto(photoServiceMock, successResult);

            repositoryMock.Setup(r => r.SaveChangesAsync()).
                ThrowsAsync(new Exception());

            var result = await handler.
                Handle(new DeleteUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to delete the user {user.Name}", result.ErrorMessage);
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

        private static void SetUpDeleteAnimalPhoto(
            Mock<IPhotoService> photoServiceMock,
            Result<Unit> result)
        {
            photoServiceMock.
                Setup(ps => ps.DeleteAnimalPhotoAsync(
                    It.IsAny<Photo>())).
                    ReturnsAsync(result);
        }

        private static void SetUpDeleteUserPhoto(
            Mock<IPhotoService> photoServiceMock,
            Result<Unit> result)
        {
            photoServiceMock.
                Setup(ps => ps.DeleteUserPhotoAsync(
                It.IsAny<Photo>(), 
                It.IsAny<string>())).
                ReturnsAsync(result);
        }
    }
}
