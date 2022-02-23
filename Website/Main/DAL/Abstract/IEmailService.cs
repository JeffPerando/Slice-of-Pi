
namespace Main.DAL.Abstract
{
    public interface IEmailService
    {
        void LogIn();
        
        void SendTextEmail(string receiver, string receiverName, string subject, string content);

        void LogOut();

    }

}
