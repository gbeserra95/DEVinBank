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
            Accounts.Add(this);
        }

        public override bool MakeWithdrawal(decimal? amount, DateTime date, DateTime time, string? note)
        {
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. A quantia para saque deve ser positiva!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            if (Balance - amount < -MonthlyIncome * 0.1m)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. Você excedeu seu limite do cheque especial!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            Balance -= amount;
            var withdrawal = new Transaction(AccNumber, -amount, date.ToString("dd/MM/yyyy"), time.ToString("HH:mm:ss"), note);
            Transactions.Add(withdrawal);

            return true;
        }
    }
}
