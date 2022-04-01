using Main.Services.Abstract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CSVBuildTests
    {
        private readonly ICSVBuilder bldr;// = new CSVBuilder();

        [SetUp]
        public void Setup() {}

        [TearDown]
        public void TearDown() {}

        [Test]
        public void CSVBuilder_CanBuildCSV()
        {
            bldr.addRow(new object[] {1, 2, 3});
            bldr.addRow(new object[] {2, 4, 6});

            Assert.That(bldr.ToString().Equals("A,B,C\n1,2,3\n2,4,6"));

        }

        [Test]
        public void CSVBuilder_MismatchColumnsThrowExcept()
        {
            Assert.Throws(Throws.Exception, () => {
                bldr.addRow(new object[] { 1, 2, 3, 4 });
            });

        }


    }

}
