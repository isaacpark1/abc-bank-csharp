using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            //Act
            Transaction t = new Transaction(5);

            //Assert
            Assert.AreEqual(5, t.Amount);
        }
    }
}
