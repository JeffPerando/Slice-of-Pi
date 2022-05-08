
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Models;
using Main.Models.FBI;
using Main.Services.Concrete;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class BackendTests
    {
        private static ICrimeAPIv2 MockCrimeAPI(List<StateCrimeStats> crimes = null)
        {
            var mock = new Mock<MockFBIService>();
            mock.CallBase = true;

            mock.Setup(e => e.StateCrimeMulti(It.IsAny<List<State>>(), It.IsAny<int?>()))
                .Returns(crimes);

            return mock.Object;
        }

        [SetUp]
        public void Setup() {}

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Backend_HasAllStates()
        {
            /*
            //Arrange
            var backend = new BackendService(MockCrimeAPI());

            //Act
            var states = backend.GetAllStates();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(states.Count, 51);
                Assert.AreEqual(states[0].Name, "Alabama");
                Assert.AreEqual(states[4].Name, "California");
                Assert.AreEqual(states[10].Abbrev, "GA");
                Assert.AreEqual(states[37].Abbrev, "OR");

            });
            */
        }

    }

}
