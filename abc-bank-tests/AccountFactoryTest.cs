using System;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountFactoryTest
    {
        [TestMethod]
        public void CreateCheckingAccount()
        {
            //Act
            var account = AccountFactory.CreateAccount(AccountType.Checking);

            //Assert
            Assert.AreEqual(account.GetType(), typeof(CheckingAccount));
            Assert.AreEqual(AccountType.Checking, account.GetAccountType());
        }

        [TestMethod]
        public void CreateSavingsAccount()
        {
            //Act
            var account = AccountFactory.CreateAccount(AccountType.Savings);

            //Assert
            Assert.AreEqual(account.GetType(), typeof(SavingsAccount));
            Assert.AreEqual(AccountType.Savings, account.GetAccountType());
        }

        [TestMethod]
        public void CreateMaxiSavingsAccount()
        {
            //Act
            var account = AccountFactory.CreateAccount(AccountType.MaxiSavings);

            //Assert
            Assert.AreEqual(account.GetType(), typeof(MaxiSavingsAccount));
            Assert.AreEqual(AccountType.MaxiSavings, account.GetAccountType());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAccount_Should_Throw_Exception_If_Invalid_AccountType()
        {
            //Act
            AccountFactory.CreateAccount(0);
        }
    }
}
