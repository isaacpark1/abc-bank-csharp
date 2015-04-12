using System;
using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary()
        {
            //Arrange
            var bank = new Bank();
            var john = new Customer("John");
            john.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            bank.AddCustomer(john);

            //Act
            var summary = bank.CustomerSummary();

            //Assert
            Assert.AreEqual("Customer Summary\n - John (1 account)", summary);
        }

        [TestMethod]
        public void CustomerSummary_WithTwoCustomers()
        {
            //Arrange
            var bank = new Bank();

            var john = new Customer("John");
            john.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            bank.AddCustomer(john);

            var jimmy = new Customer("Jimmy");
            jimmy.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            jimmy.OpenAccount(AccountFactory.CreateAccount(AccountType.Savings));
            bank.AddCustomer(jimmy);

            //Act
            var summary = bank.CustomerSummary();

            //Assert
            Assert.AreEqual("Customer Summary\n - John (1 account)\n - Jimmy (2 accounts)", summary);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetFirstCustomer_Should_Throw_Exception_If_No_Customer_Exists()
        {
            //Arrange
            var bank = new Bank();

            //Act
            bank.GetFirstCustomer();
        }

        [TestMethod]
        public void GetFirstCustomer_Should_Return_Name_Of_First_Customer()
        {
            //Arrange
            var bank = new Bank();
            var john = new Customer("John");
            john.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            bank.AddCustomer(john);

            var jimmy = new Customer("Jimmy");
            jimmy.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            jimmy.OpenAccount(AccountFactory.CreateAccount(AccountType.Savings));
            bank.AddCustomer(jimmy);

            //Act
            var customerName = bank.GetFirstCustomer();

            //Assert
            Assert.AreEqual("John", customerName);
        }

        [TestMethod]
        public void TotalInterestPaid_For_CheckingAccount()
        {
            //Arrange
            var bank = new Bank();
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Checking);
            var bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            //Act
            var interest = bank.TotalInterestPaid();

            //Assert
            Assert.AreEqual(0.1, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void TotalInterestPaid_For_Savings_account()
        {
            //Arrange
            var bank = new Bank();
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            //Act
            var interest = bank.TotalInterestPaid();

            //Assert
            Assert.AreEqual(2.0, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void TotalInterestPaid_For_Maxi_savings_account()
        {
            //Arrange
            var bank = new Bank();
            var checkingAccount = AccountFactory.CreateAccount(AccountType.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            //Act
            var interest = bank.TotalInterestPaid();

            //Assert
            Assert.AreEqual(150, interest, DOUBLE_DELTA);
        }
    }
}
