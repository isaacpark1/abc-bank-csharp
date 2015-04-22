using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class StatemetTest
    {
        [TestMethod]
        public void GetStatement()
        {
            //Arrange
            var checkingAccount = new Account(AccountType.Checking);
            var savingsAccount = new Account(AccountType.Savings);
            var henry = new Customer("Henry");

            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Act
            var stat = new Statement();
            var statement = stat.GetStatement(henry.GetName(), henry.GetAccounts());

            //Assert
            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", statement);
        }
    }
}
