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
    public abstract class BankAccount
    {
        readonly static int accountNumberSeed = 1000;
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

                return Decimal.Round(Convert.ToDecimal(salary.Trim()), 2);
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

                return Decimal.Round(Convert.ToDecimal(salary.Trim()), 2);
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

        public string GetAccountHistory()
        {
            var historyReport = new System.Text.StringBuilder();

            historyReport.AppendLine("Data\t\t\tQuantidade\t\t\tSaldo\t\t\tDescrição");
            foreach(var transaction in transactions)
            {
                historyReport.AppendLine($"{transaction._date.ToShortDateString()} {transaction._date.ToShortTimeString()}\t{transaction._amount}\t\t\t\t{transaction._currentBalance}\t\t\t{transaction._note}");
            }

            return historyReport.ToString();
        }

        public string _name { get; }
        public string _cpf { get; }
        public string _address { get; }
        public decimal _monthlyIncome { get; }
        public string _accNumber { get; }
        public string _branchName { get; }
        public decimal _balance { get; set; }

        public BankAccount(string name, string cpf, string address, decimal monthlyIncome, decimal initialBalance)
        {
            _name = name;
            _cpf = cpf;
            _address = address;
            _monthlyIncome = monthlyIncome;
            _balance = initialBalance;

            _accNumber = accountNumberSeed.ToString();
            accountNumberSeed++;

            _branchName = SetCustomerBranchRandomly();
            MakeDeposit(initialBalance, DateTime.Now, "Saldo inicial.");
        }

        public List<Transaction> transactions = new List<Transaction>();

        
        public virtual void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
               Console.WriteLine("A quantia para depósito deve ser positiva!");
                return;
            }
            var deposit = new Transaction(amount, date, note, _balance);
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
