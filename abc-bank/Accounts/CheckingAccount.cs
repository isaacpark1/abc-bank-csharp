namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base(AccountType.Checking) { }

        public override double InterestEarned()
        {
            var amount = SumTransactions();
            return amount * 0.001;
        }
    }
}