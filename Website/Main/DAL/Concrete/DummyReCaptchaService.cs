
using Main.DAL.Abstract;

namespace Main.DAL.Concrete
{
    public class DummyReCaptchaService : IReCaptchaService
    {
        public bool Passes(string response)
        {
            return true;
        }

    }

}
