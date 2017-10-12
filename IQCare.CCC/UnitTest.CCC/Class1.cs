using BusinessProcess.CCC.Tb;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessProcess.CCC.Triage;

namespace UnitTest.CCC
{
    [TestFixture]
    class Class1
    {
        private BPatientAdverseEventOutcome bp;

       [SetUp]
        public void setup()
        {
            bp = new BPatientAdverseEventOutcome();
        }

        [Test]
        public void CheckIfPatientAdverseEventOutcomeExists()
        {
         var x=   bp.CheckIfPatientAdverseEventOutcomeExists(2, 1,3);

            Assert.True(x > 1);

        }

        [Test]
        public void addition()
        {

           
            int a, b, c;
            a = 1;b = 1;
            c = a + b;

            Assert.True(c == 2);
        }

    }
}
