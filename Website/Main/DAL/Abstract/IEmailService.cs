
namespace Main.DAL.Abstract
{
    public interface IEmailService
    {
        void LogIn();

        Task SendTextEmail(string email, string receiverName, string subject, string content);

        bool IsLoggedIn();

        void LogOut();

    }

}
