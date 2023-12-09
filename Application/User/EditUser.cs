namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using Domain;
    using Domain.Enum;
    using Application.Service.Interfaces;
    using Persistence.Repositories;
    using Application.DTOs.User;
    using Application.Response;

    using static Common.ExceptionMessages.User;
    using static Common.FailMessages.User;
    using static Common.SuccessMessages.User;

    public class EditUser
    {
        public class EditUserCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;

            public EditUserDto User { get; set; } = null!;
        }

        public class EditUserHandler : IRequestHandler<EditUserCommand, Result<Unit>>
        {
            private readonly IRepository repository;
            private readonly IPhotoService photoService;

            public EditUserHandler(IRepository repository, IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository.GetById<User>(Guid.Parse(request.UserId));

                if (user == null)
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                Gender? gender = null;
                if (request.User.Gender != null)
                {
                    if (!Enum.TryParse(request.User.Gender, out Gender enumValue))
                    {
                        return Result<Unit>.Failure(InvalidGender);
                    }

                    gender = enumValue;
                }

                user.Name = request.User.Name;
                user.Email = request.User.Email;
                user.Address = request.User.Address;
                user.City = request.User.City;
                user.Age = request.User.Age;
                user.Education = request.User.Education;
                user.JobTitle = request.User.JobTitle;
                user.Gender = gender;
                user.Description = request.User.Description;

                try
                {
                    if (request.User.Photo != null)
                    {
                        Result<Unit> result = await this.photoService.AddUserPhotoAsync(request.User.Photo, request.UserId);

                        if (!result.IsSuccess)
                        {
                            return Result<Unit>.Failure(String.Format(FailedAddUserPhoto, user.Name));
                        }
                    }

                    await this.repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessEditUser, user.Name));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(FailedEditUser, user.Name));
                }
            }
        }
    }
}
