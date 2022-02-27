
namespace Main.Services.Abstract
{
    public interface IUserVerifier
    {
        int GenerateVerificationCode(string email);

        bool Verify(string email, int code);

    }

}
