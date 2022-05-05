
using Main.Controllers;
using Main.DAL.Abstract;
using Main.Services.Abstract;
//using System;

namespace Main.Services.Concrete
{
    struct UserCode
    {
        public int Code { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class UserCodes
    {
        private List<UserCode> _codes = new();
        private readonly TimeSpan _expiry;

        public UserCodes(TimeSpan expiry)
        {
            _expiry = expiry;
        }

        public bool Verify(int code)
        {
            DateTime now = DateTime.UtcNow;

            foreach (UserCode uc in _codes)
            {
                if (now > uc.Expiration)
                {
                    continue;
                }

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

            code.Code = Random.Shared.Next(100000, 999999);
            code.Expiration = DateTime.UtcNow + _expiry;

            _codes.Add(code);
            return code.Code;
        }

        public void Cleanup()
        {
            DateTime now = DateTime.UtcNow;

            _codes.RemoveAll(uc => now > uc.Expiration);

        }

    }

    public class UserVerifierService : IUserVerifierService
    {
        private Dictionary<string, UserCodes?> codes = new();
        private IEmailService _emails;
        private readonly string emailContent;
        private readonly TimeSpan _expiry;

        public UserVerifierService(IEmailService emails, string emailTemplate = "", TimeSpan? expiry = null)
        {
            _emails = emails;

            if (string.IsNullOrEmpty(emailTemplate))
            {
                emailTemplate = new FormController().ReadForm("emailconfirm");
            }

            emailContent = emailTemplate;
            _expiry = expiry ?? new TimeSpan(hours: 0, minutes: 10, seconds: 0);

        }

        public int GenerateVerificationCode(string email)
        {
            UserCodes? userCodes = codes.GetValueOrDefault(email, null);

            if (userCodes == null)
            {
                userCodes = new(_expiry);
                codes.Add(email, userCodes);

            }

            var code = userCodes.GenerateNewCode();
            
            _emails.SendTextEmail(email, "", "Confirm Your Email!", emailContent.Replace("{CODE}", code.ToString()));

            return code;
        }

        public bool Verify(string email, int code)
        {
            bool result = false;
            UserCodes? userCodes = codes.GetValueOrDefault(email, null);

            if (userCodes != null)
            {
                result = userCodes.Verify(code);

                if (result)
                {
                    codes.Remove(email);

                }

            }

            foreach (var ucs in codes.Values)
            {
                //TODO replace this with a system that does internal checking on occasion
                ucs?.Cleanup();

            }

            return result;
        }

        public void ClearAllCodes()
        {
            codes.Clear();
        }

    }

}
