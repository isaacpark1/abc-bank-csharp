using System;
using System.Linq;

namespace abc_bank.Accounts
{
    public class MaxiSavingsAccount : Account
    {
        public MaxiSavingsAccount() : base(AccountType.MaxiSavings) { }

        public override double InterestEarned()
        {
            var amount = SumTransactions();
            if (Transactions == null || Transactions.Count == 0)
                return 0;

            var allWithdrawals = Transactions.Where(x => x.Amount < 0);
            var currentDate = DateTime.Now;
            if (allWithdrawals.Any(x => (currentDate - x.TransactionDate).Days <= 10))
                return amount * 0.001;
            else
                return amount * 0.05;
        }
    }
}