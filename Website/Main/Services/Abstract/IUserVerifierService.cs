
namespace Main.Services.Abstract
{
    public interface IUserVerifierService
    {
        int GenerateVerificationCode(string email);

        bool Verify(string email, int code);

        void ClearAllCodes();

    }

}
