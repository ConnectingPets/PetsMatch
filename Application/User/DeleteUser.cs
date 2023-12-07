namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;

    using static Common.ExceptionMessages.User;
    using static Common.SuccessMessages.User;
    using static Common.FailMessages.User;

    public class DeleteUser
    {
        public class DeleteUserCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;
        }

        public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public DeleteUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository
                    .All<User>(u => u.Id.ToString() == request.UserId)
                    .Include(u => u.UsersPassions)
                    .Include(u => u.Animals)
                        .ThenInclude(a => a.AnimalMatches)
                            .ThenInclude(am => am.Match)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                DeleteAllData(user);

                try
                {
                    await this.repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessDeleteUser, user.Name));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(FailedDeleteUser, user.Name));
                }
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