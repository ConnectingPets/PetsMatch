namespace Tests.Message
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Message.ChatHistory;

    [TestFixture]
    public class ChatHistoryTests
    {
        private Mock<IRepository> repositoryMock;
        private ChatHistoryHandler handler;
        private Domain.Match match;
        private Message message;
        private ChatHistoryQuery command;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new ChatHistoryHandler(repositoryMock.Object);
            match = new Domain.Match()
            {
                MatchOn = DateTime.Now,
                AnimalMatches = new List<AnimalMatch>()
                {
                    new AnimalMatch()
                    {
                          Animal = new Animal()
                          {
                               Age = 1,
                               AnimalStatus = AnimalStatus.ForSwiping,
                               BreedId = 1,
                               Gender  = Gender.Female,
                               HealthStatus = HealthStatus.Dewormed,
                               IsEducated = false,
                               IsHavingValidDocuments = false,
                               Name = "TestAnimal",
                               OwnerId =
                               Guid.Parse("d2c2013f-6d40-49fc-9da2-1016617f681f")
                          }
                    }
                }
            };
            message = new Message()
            {
                AnimalId = Guid.NewGuid(),
                Content = "Hello test",
                MatchId = match.MatchId,
                SentOn = DateTime.Now,
            };
            command = new ChatHistoryQuery()
            {
                MatchId = Guid.NewGuid().ToString(),
                UserId = "d2c2013f-6d40-49fc-9da2-1016617f681f",
            };
        }

        [Test]
        public async Task Handle_ReturnsCorrectData_WhenGivingCorrectData()
        {
            var queryableMatch = new List<Domain.Match> { match }.AsQueryable();
            SetUpReturningAnimal(repositoryMock, queryableMatch);

            var queryableMessage = 
                new List<Message> { message}.AsQueryable();
            SetUpReturningMessage(repositoryMock, queryableMessage);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(message.Content, result.Data.First().Content);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenAnimalsNotMatched()
        {
            var queryableMatch = new List<Domain.Match>().AsQueryable();
            SetUpReturningAnimal(repositoryMock, queryableMatch);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animals are not matched", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ReturnsErrorResult_WhenUserIsNotOwner()
        {
            var queryableMatch = new List<Domain.Match>() { match}.AsQueryable();
            SetUpReturningAnimal(repositoryMock, queryableMatch);

            command.UserId = Guid.NewGuid().ToString(); 

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        private static void SetUpReturningAnimal(
             Mock<IRepository> repositoryMock,
             IQueryable<Domain.Match> queryable)
        {
            var asyncEnumerable =
                new TestAsyncEnumerableEfCore<Domain.Match>(queryable);

            repositoryMock.
                Setup(r => r.
                All(It.IsAny<Expression<Func<Domain.Match, bool>>>())).
                Returns(asyncEnumerable);
        }

        private static void SetUpReturningMessage(
             Mock<IRepository> repositoryMock,
             IQueryable<Message> queryable)
        {
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<Message>(queryable);

            repositoryMock.
                Setup(r => r.
                AllReadonly(It.IsAny<Expression<Func<Message, bool>>>())).
                Returns(asyncEnumerable);
        }
    }
}
