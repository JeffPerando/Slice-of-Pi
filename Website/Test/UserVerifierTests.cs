
using NUnit.Framework;
using Main.DAL.Concrete;
using System;
using Main.Services.Abstract;
using Main.Services.Concrete;
using System.Threading;

namespace Test
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class UserVerifierTests
    {
        private IUserVerifierService? verifier;
        private DummyEmailService emailService = new();
        
        [SetUp]
        public void Setup()
        {
            verifier = new UserVerifierService(emailService, "{CODE}", new TimeSpan(0, 0, 0, 0, 500));

        }

        [TearDown]
        public void TearDown()
        {
            verifier.ClearAllCodes();
        }

        [Test]
        public void UserVerifier_GeneratedCodeWorks()
        {
            var code = verifier.GenerateVerificationCode("a");
            Assert.That(verifier.Verify("a", code));
        }

        [Test]
        public void UserVerifier_EmailContainsCode()
        {
            var code = verifier.GenerateVerificationCode("a");
            Assert.That(emailService.LastEmailSent.Contains(code.ToString()));
        }

        [Test]
        public void UserVerifier_CodesExpire()
        {
            var code = verifier.GenerateVerificationCode("a");
            Thread.Sleep(700);
            Assert.That(!verifier.Verify("a", code));
        }

        [Test]
        public void UserVerifier_CodeACannotVerifyB()
        {
            var aCode = verifier.GenerateVerificationCode("a");
            var bCode = verifier.GenerateVerificationCode("b");
            Assert.That(!verifier.Verify("a", bCode));
        }

        [Test]
        public void UserVerifier_MultipleVerifies()
        {
            var aCode = verifier.GenerateVerificationCode("a");
            var bCode = verifier.GenerateVerificationCode("b");
            Assert.Multiple(() =>
            {
                Assert.That(verifier.Verify("a", aCode));
                Assert.That(verifier.Verify("b", bCode));
            });
        }

    }

}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
