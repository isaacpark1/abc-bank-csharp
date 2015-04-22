using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
 
        [TestMethod]
        public void TestOneAccount()
        {
            //Arrange
            var oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(AccountType.Savings));

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(1, numberOfAccounts);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            //Arrange
            var oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(AccountType.Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(2, numberOfAccounts);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            //Arrange
            var oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(AccountType.Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));
            oscar.OpenAccount(new Account(AccountType.MaxiSavings));

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(3, numberOfAccounts);
        }

        [TestMethod]
        public void Transfer()
        {
            //Arrange
            var checkingAccount = new Account(AccountType.Checking);
            var savingsAccount = new Account(AccountType.Savings);

            var henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(4000.0);
            savingsAccount.Deposit(200.0);

            //Act
            henry.Transfer(AccountType.Checking, AccountType.Savings, 100);

            //Assert
            Assert.AreEqual(300, savingsAccount.SumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_Should_Throw_Exception_If_Destination_AccountType_DoesNot_Exist()
        {
            //Arrange
            var checkingAccount = new Account(AccountType.Checking);

            var henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);

            checkingAccount.Deposit(4000.0);

            //Act
            henry.Transfer(AccountType.Checking, AccountType.Savings, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_Should_Throw_Exception_If_Source_AccountType_DoesNot_Exist()
        {
            //Arrange
            var savingAccount = new Account(AccountType.Savings);

            var henry = new Customer("Henry");
            henry.OpenAccount(savingAccount);

            savingAccount.Deposit(4000.0);

            //Act
            henry.Transfer(AccountType.Checking, AccountType.Savings, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Transfer_Should_Throw_Exception_If_There_Is_Insufficient_Funds_to_Transfer()
        {
            //Arrange
            var checkingAccount = new Account(AccountType.Checking);
            var savingsAccount = new Account(AccountType.Savings);

            var henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(200.0);

            //Act
            henry.Transfer(AccountType.Checking, AccountType.Savings, 200);
        }
    }
}
