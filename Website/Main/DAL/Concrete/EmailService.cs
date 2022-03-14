
using Main.DAL.Abstract;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class EmailService : IEmailService
    {
        private SmtpClient client = new SmtpClient();
        private string _sender;
        private string _email;
        private string _password;

        public EmailService(string sender, string email, string password)
        {
            _sender = sender;
            _email = email;
            _password = password;
        }

        public void LogIn()
        {
            try
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(_email, _password);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }

        public async Task<string> SendTextEmail(string receiver, string receiverName, string subject, string content)
        {
            if (string.IsNullOrEmpty(receiver))
            {
                throw new ArgumentException("No receiver provided!");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("No content provided!");
            }

            if (!IsLoggedIn())
            {
                LogIn();
            }

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_sender, _email));
            msg.To.Add(new MailboxAddress(receiverName, receiver));
            msg.Subject = subject;
            msg.Body = new TextPart("html") { Text = content };

            return await client.SendAsync(msg);
        }

        public bool IsLoggedIn()
        {
            return client.IsSigned;
        }

        public void LogOut()
        {
            client.Disconnect(true);

        }

    }

}

