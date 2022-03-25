using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type) : base(name, cpf, address, monthlyIncome, branch, initialBalance, type)
        {
            Accounts.Add(this);
        }

        /*
        private static string? SimulateInterestsInMonths()
        {

        }
        */
    }
}
