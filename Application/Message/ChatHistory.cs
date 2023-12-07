namespace Application.Message
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Message;
    using Application.Response;

    using static Common.ExceptionMessages.Match;

    public class ChatHistory
    {
        public class ChatHistoryQuery : IRequest<Result<IEnumerable<ChatMessageDto>>>
        {
            public string MatchId { get; set; } = null!;
        }

        public class ChatHistoryHandler : IRequestHandler<ChatHistoryQuery, Result<IEnumerable<ChatMessageDto>>>
        {
            private readonly IRepository repository;

            public ChatHistoryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<ChatMessageDto>>> Handle(ChatHistoryQuery request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Match>(m => m.MatchId.ToString() == request.MatchId) == false)
                {
                    return Result<IEnumerable<ChatMessageDto>>.Failure(MatchNotFound);
                }

                IEnumerable<ChatMessageDto> chatHistory = await this.repository
                    .AllReadonly<Message>(m => m.MatchId.ToString() == request.MatchId)
                    .OrderBy(m => m.SentOn)
                    .Select(m => new ChatMessageDto
                    {
                        AnimalId = m.AnimalId.ToString(),
                        Content = m.Content,
                        SentOn = m.SentOn
                    })
                    .ToListAsync();

                return Result<IEnumerable<ChatMessageDto>>.Success(chatHistory);
            }
        }
    }
}
