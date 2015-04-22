using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface ICustomer
    {
        string CustomerSummary();
        string GetName();
        int GetNumberOfAccounts();
        void AddCustomer(Customer customer);
    }
}
