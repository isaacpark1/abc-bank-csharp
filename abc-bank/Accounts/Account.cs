using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank.Accounts
{
    public abstract class  Account
    {
        private readonly AccountType _accountType;
        protected readonly List<Transaction> Transactions;

        protected Account(AccountType accountType)
        {
            _accountType = accountType;
            Transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            Transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            var total = SumTransactions();
            if (total < amount)
                throw new Exception("Insufficient Funds");

            Transactions.Add(new Transaction(-amount));
        }

        public abstract double InterestEarned();

        public double SumTransactions()
        {
            var amount = 0.0;

            if (Transactions == null || Transactions.Count == 0)
                return amount;

            return Transactions.Sum(t => t.Amount);
        }

        public AccountType GetAccountType()
        {
            return _accountType;
        }

        public List<Transaction> GetTransactions()
        {
            return Transactions;
        }
    }
}
