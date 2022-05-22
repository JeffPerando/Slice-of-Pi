
using Main.DAL.Abstract;

namespace Main.DAL.Mock
{
    public class MockEmailService : IEmailService
    {
        public virtual bool IsLoggedIn()
        {
            throw new NotImplementedException();
        }

        public virtual void LogIn()
        {
            throw new NotImplementedException();
        }

        public virtual void LogOut()
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> SendTextEmail(string email, string receiverName, string subject, string content)
        {
            throw new NotImplementedException();
        }

    }

}
