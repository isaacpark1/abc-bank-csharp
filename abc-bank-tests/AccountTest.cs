using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Deposit()
        {
            //Arrange
            var account = new Account(AccountType.Checking);

            //Act
            account.Deposit(200.0);
            account.Deposit(300.0);

            //Assert
            Assert.AreEqual(500, account.SumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deposit_Should_Throw_Exception_If_Amount_Is_Negative()
        {
            //Arrange
            var account = new Account(AccountType.Checking);

            //Act
            account.Deposit(-1);
        }

        [TestMethod]
        public void Withdraw()
        {
            //Arrange
            var account = new Account(AccountType.Checking);

            //Act
            account.Deposit(400.0);
            account.Withdraw(300.0);

            //Assert
            Assert.AreEqual(100, account.SumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_Should_Throw_Exception_If_Amount_Is_Negative()
        {
            //Arrange
            var account = new Account(AccountType.Checking);

            //Act
            account.Withdraw(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Withdraw_Should_Throw_Exception_If_There_Is_InsufficientFunds()
        {
            //Arrange
            var account = new Account(AccountType.Checking);
            account.Deposit(100);

            //Act
            account.Withdraw(200);
        }

        [TestMethod]
        public void InterestEarnedForCheckingAccount()
        {
            //Arrange
            var account = new Account(AccountType.Checking);
            account.Deposit(400.0);
            account.Withdraw(300.0);

            //Act
            var interest = account.InterestEarned();

            //Assert
            Assert.AreEqual(0.1, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarnedForSavingsAccount()
        {
            //Arrange
            var account = new Account(AccountType.Savings);
            account.Deposit(2000.0);
            account.Withdraw(300.0);

            //Act
            var interest = account.InterestEarned();

            //Assert
            Assert.AreEqual(2.4, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarnedForMaxiSavingsAccount()
        {
            //Arrange
            var account = new Account(AccountType.MaxiSavings);
            account.Deposit(4000.0);
            account.Withdraw(300.0);

            //Act
            var interest = account.InterestEarned();

            //Assert
            Assert.AreEqual(3.7, interest, DOUBLE_DELTA);
        }
    }
}
