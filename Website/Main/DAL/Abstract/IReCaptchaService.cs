
namespace Main.DAL.Abstract
{
    public interface IReCaptchaService
    {
        bool Passes(string response);

    }
}
