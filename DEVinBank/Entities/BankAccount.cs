using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DEVinBank.Validations;
using DEVinBank.Screens;

namespace DEVinBank.Classes
{
    public class BankAccount
    {
        protected static List<Transaction> transactionsLog = new();
        protected static List<BankAccount> accountsLog = new();

        private static int accountNumberSeed = 1;
        readonly static string[] branches = { "001 - Florianópolis", "002 - São José", "003 - Biguaçu" };

        public static string? SetCustomerName()
        {
            Console.Write("Digite o nome do titular da conta: ");

            try
            {
                string? name = Console.ReadLine();
                
                if (name == null)
                {
                    throw new Exception();
                }

                return name.Trim();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNome do titular inválido!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? name = Console.ReadLine();

                if (name == null)
                {
                    return null;
                }

                return name.Trim();
            }
        }

        public static string? SetCustomerCPF()
        {
            Console.Write("Digite o CPF do titular da conta (apenas dígitos): ");

            try
            {
                string? cpf = Console.ReadLine();

                if (!Validate.ValidateCPF(cpf))
                    throw new Exception();

                return cpf;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCPF inválido!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? cpf = Console.ReadLine();

                if (!Validate.ValidateCPF(cpf))
                    return null;

                return cpf;
            }
        }

        public static string? SetCustomerAddress()
        {
            Console.Write("Digite o endereço do titular da conta: ");

            try
            {
                string? address = Console.ReadLine();

                if (address == null)
                {
                    throw new Exception();
                }

                return address.Trim();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEndereço inválido!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? address = Console.ReadLine();

                if (address == null)
                {
                    return null;
                }

                return address.Trim();
            }
        }

        public static decimal? SetCustomerMonthlyIncome()
        {
            Console.Write("Digite a renda mensal do titular da conta: R$");

            try
            {
                string? salary = Console.ReadLine();

                if (salary == null)
                    throw new Exception();

                decimal decimalSalary = Decimal.Round(Convert.ToDecimal(salary.Trim()), 2);

                if (decimalSalary <= 0)
                    throw new Exception();

                return decimalSalary;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nRenda mensal inválida!");
                Console.Write("Digite novamente: R$");
                Console.ResetColor();

                string? salary = Console.ReadLine();

                if (salary == null)
                    return null;

                decimal decimalSalary = Decimal.Round(Convert.ToDecimal(salary.Trim()), 2);

                if (decimalSalary <= 0)
                    return null;

                return decimalSalary;
            }
        }

        public static string? SetCustomerBranch()
        {
            Console.WriteLine("Escolha sua agência de preferência:");
            Console.WriteLine("1 - Florianópolis");
            Console.WriteLine("2 - São José");
            Console.WriteLine("3 - Biguaçu");
            Console.Write("Digite a opção desejada: ");

            try
            {
                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && Menu.CheckInputRange(1, 3, option))
                {
                    return branches[option - 1];
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAgência inválida!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && Menu.CheckInputRange(1, 3, option))
                {
                    return branches[option - 1];
                }
                
                return null;
            }
        }
        public static decimal? SetInitialBalance()
        {
            Console.Write("Digite o saldo inicial desta conta: R$");

            try
            {
                string? initialBalance = Console.ReadLine();

                if (initialBalance == null)
                    throw new Exception();

                decimal decimalInitialBalance = Decimal.Round(Convert.ToDecimal(initialBalance.Trim()), 2);

                if (decimalInitialBalance <= 0)
                    throw new Exception();

                return decimalInitialBalance;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nRenda mensal inválida!");
                Console.Write("Digite novamente: R$");
                Console.ResetColor();

                string? initialBalance = Console.ReadLine();

                if (initialBalance == null)
                    return null;

                decimal decimalInitialBalance = Decimal.Round(Convert.ToDecimal(initialBalance.Trim()), 2);

                if (decimalInitialBalance <= 0)
                    return null;

                return decimalInitialBalance;
            }
        }

        public static string ListAllAccounts()
        {
            var accountsReport = new System.Text.StringBuilder();

            foreach(var account in accountsLog)
            {
                accountsReport.AppendLine($"Titular: {account.Name} | CPF: {account.CPF} | {account.Type}: {account.AccNumber} | Agência: {account.Branch}");
            }

            return accountsReport.ToString();
        }

        public static string ShowAccountHistory()
        {
            var historyReport = new System.Text.StringBuilder();

            historyReport.AppendLine("Data\t\t\tQuantidade\t\t\tSaldo\t\t\tDescrição");
            foreach(var transaction in transactionsLog)
            {
                historyReport.AppendLine($"{transaction.Date.ToShortDateString()} {transaction.Date.ToShortTimeString()}\t{transaction.Amount}\t\t\t\t{transaction.CurrentBalance}\t\t\t{transaction.Note}");
            }

            return historyReport.ToString();
        }

        public string? Name { get; set; }
        public string? CPF { get; }
        public string? Address { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public string? AccNumber { get; private set; }
        public string? Branch { get; set; }
        public decimal? Balance { get; set; }
        public string? Type { get; set; }

        public BankAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type)
        {
            Name = name;
            CPF = cpf;
            Address = address;
            MonthlyIncome = monthlyIncome;
            Balance = initialBalance;

            AccNumber = accountNumberSeed.ToString();
            accountNumberSeed++;

            Branch = branch;
            Type = type;
            MakeDeposit(initialBalance, DateTime.Now, "Saldo inicial.");
        }
        public void MakeDeposit(decimal? amount, DateTime date, string? note)
        {
            if (amount <= 0)
            {
                Console.WriteLine("A quantia para depósito deve ser positiva!");
                return;
            }
            var deposit = new Transaction(amount, date, note, Balance);
            transactionsLog.Add(deposit);
        }

        public virtual void RegisterAccount()
        {
        }
        public virtual void MakeWithdrawal(decimal? amount, DateTime date, string? note)
        {
            if (amount <= 0)
            {
                Console.WriteLine("A quantia para saque deve ser positiva!");
                return;
            }

            if (Balance - amount < 0)
            {
                Console.WriteLine("Você não possui fundos suficientes!");
                return;
            }

            Balance -= amount;
            var withdrawal = new Transaction(-amount, date, note, Balance);
            transactionsLog.Add(withdrawal);
        }

        public void MakeTransfer()
        {

        }

        public void ChangeCustomerData()
        {

        }
    }
             
}
