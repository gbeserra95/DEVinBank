using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Classes
{
    public class CheckingAccount : BankAccount
    {
        public CheckingAccount(string name, string cpf, string address, decimal monthlyIncome, decimal initialBalance) : base(name, cpf, address, monthlyIncome, initialBalance)
        {
        }

        public override void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                Console.WriteLine("A quantia para saque deve ser positiva!");
                return;
            }

            if (_balance - amount < -_monthlyIncome * 0.1m)
            {
                Console.WriteLine("Você excedeu seu limite do cheque especial!");
                return;
            }
            
            _balance -= amount;
            var withdrawal = new Transaction(-amount, date, note, _balance);
            transactions.Add(withdrawal);
        }
    }
}
