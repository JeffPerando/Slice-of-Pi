
using NUnit.Framework;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Microsoft.Extensions.Configuration;
using NuGet.Configuration;
using System;

namespace Test
{
    public class EmailTests
    {
        private string defaultReceiver = "jbrusseperando18@mail.wou.edu";
        private IEmailService? email;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
            .AddUserSecrets<EmailTests>()
            .Build();

            System.Diagnostics.Debug.WriteLine(configuration["EmailPW"]);
            
            email = new EmailService("Slice of Pi Unit Tests", "sliceofpi.cs46x", configuration["EmailPW"]);

            email.LogIn();

        }

        [TearDown]
        public void Clean()
        {
            if (email != null)
            {
                email.LogOut();
            }
        }

        [Test]
        public void Test_SendsEmail()
        {
            Assert.DoesNotThrow(() => {
                email.SendTextEmail(defaultReceiver, "", "Unit testing", "Hi! If you're receiving this, then the unit test passed!");
            });

        }

        [Test]
        public void Test_NoEmptyReceiver()
        {
            Assert.Throws<ArgumentException>(() => {
                email.SendTextEmail("", "Mr. Pilkington", "Test_NoEmptyReceiver", "This shouldn't pass...");
            });

        }

        [Test]
        public void Test_NoEmptyContent()
        {
            Assert.Throws<ArgumentException>(() => {
                email.SendTextEmail(defaultReceiver, "John Doe", "Test_NoEmptyContent email", "");
            });

        }

    }

}
