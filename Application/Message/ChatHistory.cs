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
    using static Common.ExceptionMessages.Animal;

    public class ChatHistory
    {
        public class ChatHistoryQuery : IRequest<Result<IEnumerable<ChatMessageDto>>>
        {
            public string MatchId { get; set; } = null!;

            public string UserId { get; set; } = null!;
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
                Match? match = await this.repository.All<Match>(m => m.MatchId.ToString() == request.MatchId.ToLower())
                    .Include(m => m.AnimalMatches)
                    .ThenInclude(am => am.Animal)
                    .FirstOrDefaultAsync();

                if (match == null)
                {
                    return Result<IEnumerable<ChatMessageDto>>.Failure(MatchNotFound);
                }

                bool isOwner = match.AnimalMatches.Any(am => am.Animal.OwnerId.ToString() == request.UserId.ToLower());

                if (!isOwner)
                {
                    return Result<IEnumerable<ChatMessageDto>>.Failure(NotOwner);
                }

                IEnumerable<ChatMessageDto> chatHistory = await this.repository
                    .AllReadonly<Message>(m => m.MatchId.ToString() == request.MatchId.ToLower())
                    .OrderBy(m => m.SentOn)
                    .Select(m => new ChatMessageDto
                    {
                        AnimalId = m.AnimalId.ToString(),
                        Content = m.Content,
                        SentOn = m.SentOn.ToString()
                    })
                    .ToListAsync();

                return Result<IEnumerable<ChatMessageDto>>.Success(chatHistory);
            }
        }
    }
}
