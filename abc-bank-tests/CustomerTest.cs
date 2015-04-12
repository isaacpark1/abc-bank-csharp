using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            //Arrange
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Checking);
            var savingsAccount = AccountFactory.CreateAccount(AccountType.Savings);

            var henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Act
            var statement = henry.GetStatement();

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

        [TestMethod]
        public void TestOneAccount()
        {
            //Arrange
            var oscar = new Customer("Oscar");
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.Savings));

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
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.Savings));
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));

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
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.Savings));
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.Checking));
            oscar.OpenAccount(AccountFactory.CreateAccount(AccountType.MaxiSavings));

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(3, numberOfAccounts);
        }

        [TestMethod]
        public void Transfer()
        {
            //Arrange
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Checking);
            var savingsAccount = AccountFactory.CreateAccount(AccountType.Savings);

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
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Checking);

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
            var savingAccount = AccountFactory.CreateAccount(AccountType.Savings);

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
            var checkingAccount = AccountFactory.CreateAccount(AccountType.Checking);
            var savingsAccount = AccountFactory.CreateAccount(AccountType.Savings);

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
