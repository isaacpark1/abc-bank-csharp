using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double Amount;

        public DateTime TransactionDate;

        public Transaction(double amount)
        {
            Amount = amount;
            TransactionDate = DateTime.Now;
        }
    }
}
