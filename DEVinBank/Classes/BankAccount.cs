using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Classes
{
    public class BankAccount
    {
        private static int accountNumberSeed = 1000;

        // Create a function to validate CPF (Here or outside)?

        private static string SetCustomerBranchRandomly()
        {
            int n = new Random().Next(0, 4);

            if (n == 1)
            {
                return "001 - Florianópolis";
            }

            if (n == 2)
            {
                return "002 - São Jósé";
            }

            return "003 - Biguaçu";
        }

        public string GetAccountHistory()
        {
            var historyReport = new System.Text.StringBuilder();

            double balance = 0;
            historyReport.AppendLine("Data\t\t\tQuantidade\t\t\tSaldo\t\t\tDescrição");
            foreach(var transaction in transactions)
            {
                balance += transaction._amount;
                historyReport.AppendLine($"{transaction._date.ToShortDateString()}\t\t{transaction._amount}\t\t{_balance}\t\t{transaction._note}");
            }

            return historyReport.ToString();
        }

        public string _name { get; }
        public string _cpf { get; }
        public string _address { get; }
        public double _monthlyIncome { get; }
        public string _accNumber { get; }
        public string _branchName { get; }
        public double _balance
        {
            get
            {
                double balance = 0;
                foreach (var item in transactions)
                {
                    balance += item._amount;
                }

                return balance;
            }
        }

        public BankAccount(string name, string cpf, string address, double monthlyIncome, double initialBalance)
        {
            _name = name;
            _cpf = cpf;
            _address = address;
            _monthlyIncome = monthlyIncome;

            _accNumber = accountNumberSeed.ToString();
            accountNumberSeed++;

            _branchName = SetCustomerBranchRandomly();
            MakeDeposit(initialBalance, DateTime.Now, "Saldo inicial.");
        }

        private List<Transaction> transactions = new List<Transaction>();

        
        public void MakeWithdrawal(double amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "A quantia para saque deve ser positiva!");
            }

            if (_balance - amount < 0)
            {
                throw new InvalidOperationException("Fundos insuficientes para saque!");
            }

            var withdrawal = new Transaction(-amount, date, note);
            transactions.Add(withdrawal);
        }

        public void MakeDeposit(double amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "A quantia para depósito deve ser positiva!");
            }
            var deposit = new Transaction(amount, date, note);
            transactions.Add(deposit);
        }

        public void ShowHistory()
        {

        }

        public void MakeTransfer()
        {

        }

        public void ChangeCustomerData()
        {

        }
    }
             
}
