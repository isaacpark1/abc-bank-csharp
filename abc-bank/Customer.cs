using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abc_bank.Accounts;

namespace abc_bank
{
    public class Customer
    {
        private readonly string _name;
        private readonly List<Account> _accounts;

        public Customer(string name)
        {
            _name = name;
            _accounts = new List<Account>();
        }

        public string GetName()
        {
            return _name;
        }

        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }

        public void Transfer(AccountType sourceAccountType, AccountType destinationAccountType, double amount)
        {
            if (_accounts == null || _accounts.Count == 0)
                throw new Exception("Customer does not have any account.");

            var sourceAccount = _accounts.SingleOrDefault(x => x.GetAccountType() == sourceAccountType);
            if (sourceAccount == null)
                throw new ArgumentException("source account does not exist");

            var destinationAccount = _accounts.SingleOrDefault(x => x.GetAccountType() == destinationAccountType);
            if (destinationAccount == null)
                throw new ArgumentException("destination account does not exist");

            if (amount <= 0)
                throw new ArgumentException("amount must be greater than zero");

            sourceAccount.Withdraw(amount);
            destinationAccount.Deposit(amount);
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (var a in _accounts)
                total += a.InterestEarned();
            return total;
        }

       public List<Account> GetAccounts()
        {
            return _accounts;
        }
    }
}
