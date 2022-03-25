using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DEVinBank.Validations;
using DEVinBank.Screens;

namespace DEVinBank.Entities
{
    public class BankAccount
    {
        protected static List<Transaction> transactionsLog = new();
        protected static List<BankAccount> accountsLog = new();

        private static int accountNumberSeed = 1;
        readonly static string[] branches = { "001 - Florianópolis", "002 - São José", "003 - Biguaçu" };

        public static decimal? CheckCurrencyInput(string instructionMessage, string errorMessage)
        {
            Console.Write(instructionMessage);

            try
            {
                string? value = Console.ReadLine();

                if (value == null)
                    throw new Exception();

                decimal decimalValue = Decimal.Round(Convert.ToDecimal(value.Trim()), 2);

                if (decimalValue <= 0)
                    throw new Exception();

                return decimalValue;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{errorMessage}");
                Console.ResetColor();
                Console.Write("Digite novamente: R$");

                string? value = Console.ReadLine();

                if (value == null)
                    return null;

                decimal decimalValue = Decimal.Round(Convert.ToDecimal(value.Trim()), 2);

                if (decimalValue <= 0)
                    return null;

                return decimalValue;
            }
        }
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
            return CheckCurrencyInput("Digite a renda mensal do titular da conta: R$", "Renda mensal inválida!");
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
            return CheckCurrencyInput("Digite o saldo inicial desta conta: R$", "Renda mensal inválida!");
        }
        public static BankAccount? GetBankAccountByAccountNumber()
        {
            try
            {
                if(accountsLog.Count == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não existem contas cadastradas neste banco.");
                    Console.ResetColor();

                    Console.WriteLine("Pressione enter para sair...");
                    Console.ReadLine();

                    return null;
                }

                Console.Clear();
                Console.Write("Digite o número da conta: ");

                string? accountNumber = Console.ReadLine();

                if (accountNumber == null)
                    throw new Exception();

                BankAccount filteredAccount = accountsLog.First(account => account.AccNumber == accountNumber.Trim());

                if (filteredAccount == null)
                {
                    throw new Exception();
                }

                return filteredAccount;

            } catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNúmero da conta inválido!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? accountNumber = Console.ReadLine();

                if (accountNumber == null)
                    return null;

                BankAccount filteredAccount = (BankAccount)accountsLog.First(account => account.AccNumber == accountNumber);

                if (filteredAccount == null)
                {
                    return null;
                }

                return filteredAccount;
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

        public virtual void MakeWithdrawal(decimal? amount, DateTime date, string? note)
        {
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. A quantia para saque deve ser positiva!");
                Console.ResetColor();
                return;
            }

            if (Balance - amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. Você não possui fundos suficientes!");
                Console.ResetColor();
                return;
            }

            Balance -= amount;
            var withdrawal = new Transaction(-amount, date, note, Balance);
            transactionsLog.Add(withdrawal);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSaque realizado com sucesso! Seu saldo é de R${Balance}");
            Console.ResetColor();
        }

        public void MakeTransfer()
        {

        }

        public void ChangeCustomerData()
        {

        }
    }
             
}
