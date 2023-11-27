namespace Application.User
{
    using Application.DTOs.User;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ShowEditUser
    {
        public class ShowEditUserQuery : IRequest<ShowEditUserDto>
        {
            public string UserId { get; set; } = null!;
        }

        public class ShowEditUserHandler : IRequestHandler<ShowEditUserQuery, ShowEditUserDto>
        {
            public async Task<ShowEditUserDto> Handle(ShowEditUserQuery request, CancellationToken cancellationToken)
            {
                ShowEditUserDto showEditUserDto = new ShowEditUserDto();

                return showEditUserDto;
            }
        }
    }
}
