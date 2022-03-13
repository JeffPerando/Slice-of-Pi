
namespace Main.DAL.Abstract
{
    public interface IReCaptchaService
    {
        Task<bool> Passes(string response);

    }
}
