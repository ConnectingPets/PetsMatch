namespace Application.Service.Interfaces
{
    using Domain;

    public interface ITokenService
    {
        string CreateToken(User user);

        string GenerateKey();
    }
}
