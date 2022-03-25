using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class CheckingAccount : BankAccount
    {
        public CheckingAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type) : base(name, cpf, address, monthlyIncome, branch, initialBalance, type)
        {
            accountsLog.Add(this);
        }

        public override void MakeWithdrawal(decimal? amount, DateTime date, string? note)
        {
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. A quantia para saque deve ser positiva!");
                Console.ResetColor();
                return;
            }

            if (Balance - amount < -MonthlyIncome * 0.1m)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. Você excedeu seu limite do cheque especial!!");
                Console.ResetColor();
                return;
            }

            Balance -= amount;
            var withdrawal = new Transaction(-amount, date, note, Balance);
            transactionsLog.Add(withdrawal);
        }
    }
}
