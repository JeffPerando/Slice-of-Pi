
using Main.DAL.Abstract;

namespace Main.DAL.Concrete
{
    public class DummyEmailService : IEmailService
    {
        public string? LastEmailSent { get; set; }

        public bool IsLoggedIn()
        {
            return true;
        }

        public void LogIn(){}

        public void LogOut(){}

        public Task SendTextEmail(string email, string receiverName, string subject, string content)
        {
            LastEmailSent = content;
            return Task.CompletedTask;
        }

    }

}
