
using Main.DAL.Abstract;

namespace Main.DAL.Concrete
{
    public class DummyReCaptchaService : IReCaptchaService
    {
        public Task<bool> Passes(string response)
        {
            return Task.FromResult(true);
        }

    }

}
