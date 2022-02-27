
using Main.DAL.Abstract;
using Main.Services.Abstract;

namespace Main.Services.Concrete
{
    struct UserCode
    {
        public int Code { get; set; }
        public DateTime Expiration { get; set; }

        public UserCode()
        {
            Code = Random.Shared.Next(100000, 999999);
            Expiration = DateTime.UtcNow.AddMinutes(5);

        }

    }

    public class UserCodes
    {
        private string _email;
        private List<UserCode> _codes = new();

        public UserCodes(string email)
        {
            _email = email;

        }

        public bool Verify(int code)
        {
            DateTime now = DateTime.UtcNow;

            foreach (UserCode uc in _codes)
            {
                if (uc.Code == code)
                {
                    return true;
                }

            }

            return false;
        }

        public int GenerateNewCode()
        {
            UserCode code = new();
            _codes.Add(code);
            return code.Code;
        }

        public void Cleanup()
        {
            DateTime now = DateTime.UtcNow;

            _codes.RemoveAll(uc => now > uc.Expiration);

        }

    }

    public class UserVerifier : IUserVerifier
    {
        private Dictionary<string, UserCodes> codes = new();
        private IEmailService _emails;

        public UserVerifier(IEmailService emails)
        {
            _emails = emails;
        }

        public int GenerateVerificationCode(string email)
        {
            UserCodes? userCodes = codes.GetValueOrDefault(email, null);

            if (userCodes == null)
            {
                userCodes = new(email);
                codes.Add(email, userCodes);

            }

            var code = userCodes.GenerateNewCode();

            _emails.SendTextEmail(email, "", "Confirm Your Email!", $"Enter the code {code} to verify your email with Slice of Pi");

            return code;
        }

        public bool Verify(string email, int code)
        {
            UserCodes? userCodes = codes.GetValueOrDefault(email, null);

            if (userCodes == null)
            {
                return false;
            }

            bool result = userCodes.Verify(code);

            if (result)
            {
                codes.Remove(email);
            }

            foreach (UserCodes ucs in codes.Values)
            {
                //TODO replace this with a system that does internal checking on occasion
                ucs.Cleanup();

            }

            return result;
        }

    }

}
