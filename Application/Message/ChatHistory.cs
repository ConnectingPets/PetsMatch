namespace Application.Message
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Message;
    using Application.Exceptions;

    public class ChatHistory
    {
        public class ChatHistoryQuery : IRequest<IEnumerable<ChatMessageDto>>
        {
            public string MatchId { get; set; } = null!;
        }

        public class ChatHistoryHandler : IRequestHandler<ChatHistoryQuery, IEnumerable<ChatMessageDto>>
        {
            private readonly IRepository repository;

            public ChatHistoryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<ChatMessageDto>> Handle(ChatHistoryQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.MatchId, out Guid guidMatchId))
                {
                    throw new MatchNotFoundException();
                }

                return await this.repository
                    .AllReadonly<Message>(m => m.MatchId == guidMatchId)
                    .OrderBy(m => m.SentOn)
                    .Select(m => new ChatMessageDto
                    {
                        AnimalId = m.AnimalId.ToString(),
                        Content = m.Content,
                        SentOn = m.SentOn
                    })
                    .ToListAsync();
            }
        }
    }
}
