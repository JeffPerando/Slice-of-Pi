
using NUnit.Framework;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Microsoft.Extensions.Configuration;
using NuGet.Configuration;
using System;

namespace Test
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class EmailTests
    {
        private string defaultReceiver = "jbrusseperando18@mail.wou.edu";
        private IEmailService? email;
        private string username = "sliceofpi.cs46x";
        private string password = "";

        [SetUp]
        public void Setup()
        {
            if (password.Length == 0)
            {
                var configuration = new ConfigurationBuilder()
                .AddUserSecrets<EmailTests>()
                .Build();

                password = configuration["EmailPW"];

            }

            email = new EmailService("Slice of Pi Unit Tests", username, password);

        }

        [TearDown]
        public void Clean()
        {
            if (email != null && email.IsLoggedIn())
            {
                email.LogOut();

            }

        }

        [Test]
        public void Test_SendsTextEmail()
        {
            var date = DateTime.Now;

            email.LogIn();
            
            Assert.That(email.IsLoggedIn(), Is.True);
            Assert.DoesNotThrow(() => {
                email.SendTextEmail(defaultReceiver, "Tester", $"Unit test from {date.ToShortTimeString()}", "Hi! If you're receiving this, then the unit test passed!");
            });

        }

        [Test]
        public void Test_NoEmptyReceiver()
        {
            email.LogIn();

            Assert.That(email.IsLoggedIn(), Is.True);
            Assert.Throws<ArgumentException>(() => {
                email.SendTextEmail("", "Mr. Pilkington", "Test_NoEmptyReceiver", "This shouldn't pass...");
            });

        }

        [Test]
        public void Test_NoEmptyContent()
        {
            email.LogIn();

            Assert.That(email.IsLoggedIn(), Is.True);
            Assert.Throws<ArgumentException>(() => {
                email.SendTextEmail(defaultReceiver, "John Doe", "Test_NoEmptyContent email", "");
            });

        }

        [Test]
        public void Test_DoubleLogInFails()
        {
            email.LogIn();
            Assert.That(email.IsLoggedIn(), Is.True);
            Assert.That(() =>
            {
                email.LogIn();
            }, Throws.Exception);

        }

    }

}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
