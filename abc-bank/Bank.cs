using System;
using System.Collections.Generic;
using System.Text;

namespace abc_bank
{
    public class Bank
    {
        private readonly List<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public string CustomerSummary()
        {
            var summary = new StringBuilder();
            summary.Append("Customer Summary");

            foreach (var c in _customers)
            {
                summary.Append(string.Format("\n - {0} ({1})", c.GetName(), FormatNumberOfAccountsString(c)));
            }
            return summary.ToString();
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private static string FormatNumberOfAccountsString(Customer c)
        {
            var numberOfAccounts = c.GetNumberOfAccounts();

            return string.Format("{0} account{1}", numberOfAccounts, numberOfAccounts > 1 ? "s" : "");
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in _customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public string GetFirstCustomer()
        {
            if (_customers == null || _customers.Count == 0)
                throw new Exception("No customer exist in the bank.");

            return _customers[0].GetName();
        }
    }
}
