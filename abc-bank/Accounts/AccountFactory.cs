using System;

namespace abc_bank.Accounts
{
    public enum AccountType : int
    {
        Checking = 1,
        Savings = 2,
        MaxiSavings = 3
    }

    public class AccountFactory
    {
        public static Account CreateAccount(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Checking:
                    return new CheckingAccount();
                case AccountType.Savings:
                    return new SavingsAccount();
                case AccountType.MaxiSavings:
                    return new MaxiSavingsAccount();
                default:
                    throw new Exception("Invalid account type.");
            }
        }
    }
}