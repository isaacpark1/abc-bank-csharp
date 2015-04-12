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

        public string GetStatement()
        {
            var statement = new StringBuilder();
            statement.Append(string.Format("Statement for {0}\n", _name));
            var total = 0.0;
            foreach (Account a in _accounts)
            {
                statement.Append(string.Format("\n{0}\n", StatementForAccount(a)));
                total += a.SumTransactions();
            }
            statement.Append(string.Format("\nTotal In All Accounts {0}", ToDollars(total)));
            return statement.ToString();
        }

        private static string StatementForAccount(Account a)
        {
            var s = new StringBuilder();

            //Translate to pretty account type
            switch (a.GetAccountType())
            {
                case AccountType.Checking:
                    s.Append("Checking Account\n");
                    break;
                case AccountType.Savings:
                    s.Append("Savings Account\n");
                    break;
                case AccountType.MaxiSavings:
                    s.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (var t in a.GetTransactions())
            {
                s.Append(string.Format("  {0} {1}\n", (t.Amount < 0 ? "withdrawal" : "deposit"), ToDollars(t.Amount)));
                total += t.Amount;
            }
            s.Append("Total " + ToDollars(total));
            return s.ToString();
        }

        private static string ToDollars(double d)
        {
            return string.Format("${0:N}", Math.Abs(d));
        }
    }
}
