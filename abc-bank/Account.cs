using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        private readonly List<Transaction> Transactions;
        private readonly AccountType _accountType;

        public Account(AccountType accountType)
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

        public double InterestEarned()
        {
            var amount = SumTransactions();

            switch (_accountType)
            {
                case AccountType.Checking:
                    return amount * 0.001;

                case AccountType.Savings:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;

                case AccountType.MaxiSavings:
                    if (Transactions == null || Transactions.Count == 0)
                        return 0;
                    var allWithdrawals = Transactions.Where(x => x.Amount < 0);
                    var currentDate = DateTime.Now;
                    if (allWithdrawals.Any(x => (currentDate - x.TransactionDate).Days <= 10))
                        return amount * 0.001;
                    else
                        return amount * 0.05;
                default:
                    return 0;
            }
        }

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
