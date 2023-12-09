namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Service.Interfaces;

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
            private readonly IPhotoService photoService;

            public DeleteUserHandler(
                IRepository repository,
                IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository
                    .All<User>(u => u.Id.ToString() == request.UserId.ToLower())
                    .Include(u => u.UsersPassions)
                    .Include(u => u.Photo)
                    .Include(u => u.Animals)
                        .ThenInclude(a => a.AnimalMatches)
                            .ThenInclude(am => am.Match)
                    .Include(u => u.Animals )
                        .ThenInclude(a => a.Photos)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                Result<Unit> result = await this.DeleteAllData(user);

                if (!result.IsSuccess)
                {
                    return result;
                }

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

            private async Task<Result<Unit>> DeleteAllData(User user)
            {
                this.repository.DeleteRange(user.UsersPassions.ToArray());

                foreach (var animal in user.Animals)
                {
                    Result<Unit> result = await this.DeleteAllAnimalPhotos(animal.Photos.ToArray());

                    if (!result.IsSuccess)
                    {
                        return result;
                    }

                    this.repository.DeleteRange<Swipe>(swipe => 
                        swipe.SwipeeAnimalId == animal.AnimalId ||
                        swipe.SwiperAnimalId == animal.AnimalId);

                    Match[] matches = animal.AnimalMatches
                        .Select(am => am.Match)
                        .ToArray();

                    DeleteAllMatches(matches);

                    this.repository.Delete(animal);
                }

                if (user.Photo != null)
                {
                    Result<Unit> result = await this.photoService.DeleteUserPhotoAsync(user.Photo, user.Id.ToString());

                    if (!result.IsSuccess)
                    {
                        return Result<Unit>.Failure(String.Format(FailedDeleteUserPhoto, user.Name));
                    }
                }

                this.repository.Delete(user);

                return Result<Unit>.Success(Unit.Value);
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

            private async Task<Result<Unit>> DeleteAllAnimalPhotos(Photo[] photos)
            {
                foreach (var photo in photos)
                {
                    Result<Unit> result = await this.photoService.DeleteAnimalPhotoAsync(photo);

                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}