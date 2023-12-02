namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Exceptions.Entity;
    using Application.Exceptions.User;

    public class DeleteUser
    {
        public class DeleteUserCommand : IRequest<Unit>
        {
            public string UserId { get; set; } = null!;
        }

        public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
        {
            private readonly IRepository repository;

            public DeleteUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.UserId, out Guid guidUserId))
                {
                    throw new InvalidGuidFormatException();
                }

                User? user = await this.repository
                    .All<User>(u => u.Id == guidUserId)
                    .Include(u => u.UsersPassions)
                    .Include(u => u.Animals)
                        .ThenInclude(a => a.AnimalMatches)
                            .ThenInclude(am => am.Match)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                DeleteAllData(user);

                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }

            private void DeleteAllData(User user)
            {
                this.repository.DeleteRange(user.UsersPassions.ToArray());

                foreach (var animal in user.Animals)
                {
                    this.repository.DeleteRange<Swipe>(swipe => 
                        swipe.SwipeeAnimalId == animal.AnimalId ||
                        swipe.SwiperAnimalId == animal.AnimalId);

                    Match[] matches = animal.AnimalMatches
                        .Select(am => am.Match)
                        .ToArray();

                    DeleteAllMatches(matches);

                    this.repository.Delete(animal);
                }

                this.repository.Delete(user);
            }

            private void DeleteAllMatches(Match[] matches)
            {
                foreach (var match in matches)
                {
                    this.repository.DeleteRange<Message>(m => m.MatchId == match.MatchId);
                    this.repository.DeleteRange<AnimalMatch>(m => m.MatchId == match.MatchId);
                    this.repository.Delete(match);
                }
            }
        }
    }
}