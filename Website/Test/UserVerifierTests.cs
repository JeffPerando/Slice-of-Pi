
using NUnit.Framework;
using Main.DAL.Concrete;
using System;
using Main.Services.Abstract;
using Main.Services.Concrete;
using System.Threading;
using Main.DAL.Mock;
using Moq;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable IDE0059 // Unnecessary assignment of a value
namespace Test
{
    public class UserVerifierTests
    {
        private IUserVerifierService? verifier;
        
        private static MockEmailService MockEmail()
        {
            Mock<MockEmailService> moq = new();
            
            moq.CallBase = true;

            moq.Setup(es => es.IsLoggedIn()).Returns(true);
            moq.Setup(es => es.LogOut());
            moq.Setup(es => es.LogIn());
            moq.Setup(es => es.SendTextEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => { return ""; });

            return moq.Object;
        }

        [SetUp]
        public void Setup()
        {
            verifier = new UserVerifierService(MockEmail(), "{CODE}", new TimeSpan(0, 0, 0, 0, 500));

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

        [Test]
        public void UserVerifier_AnyCodeWorks()
        {
            var code1 = verifier.GenerateVerificationCode("a");
            var code2 = verifier.GenerateVerificationCode("a");
            Assert.That(verifier.Verify("a", code1));
        }

        [Test]
        public void UserVerifier_CodesActuallyExpire()
        {
            var oldCode = verifier.GenerateVerificationCode("a");
            Thread.Sleep(700);
            var newCode = verifier.GenerateVerificationCode("a");
            Assert.Multiple(() =>
            {
                Assert.That(!verifier.Verify("a", oldCode));
                Assert.That(verifier.Verify("a", newCode));
            });
        }

    }

}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore IDE0059 // Unnecessary assignment of a value
