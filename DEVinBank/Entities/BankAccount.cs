using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DEVinBank.Validations;
using DEVinBank.Screens;
using DEVinBank.Enums;

namespace DEVinBank.Entities
{
    public class BankAccount
    {
        protected static List<Transaction> Transactions = new();
        protected static List<BankAccount> Accounts = new();

        private static int accountNumberSeed = 1;
        readonly static string[] branches = { "001 - Florianópolis", "002 - São José", "003 - Biguaçu" };

        public static (string name, decimal rate, int requiredMonths) LCI = ("LCI", 8m, 6);
        public static (string name, decimal rate, int requiredMonths) LCA = ("LCA", 9m, 12);
        public static (string name, decimal rate, int requiredMonths) CDB = ("CDB", 10m, 36);

        public static bool YesOrNoAnswer(string message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message} (s/n)? ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                if (input == null)
                    throw new Exception();

                if (input.Trim().ToUpper() != "SIM" && input.Trim().ToUpper() != "S" && input.Trim().ToUpper() != "NÃO" && input.Trim().ToUpper() != "NAO" && input.Trim().ToUpper() != "N")
                    throw new Exception();

                if (input.Trim().ToUpper() == "NÃO" || input.Trim().ToUpper() == "NAO" || input.Trim().ToUpper() == "N")
                    return false;

                return true;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOpção inválida!");
                Console.Write("Digite novamente (s/n): ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                if (input == null)
                    return false;

                if (input.Trim().ToUpper() != "SIM" && input.Trim().ToUpper() != "S" && input.Trim().ToUpper() != "NÃO" && input.Trim().ToUpper() != "NAO" && input.Trim().ToUpper() != "N")
                    return false;

                if (input.Trim().ToUpper() == "NÃO" || input.Trim().ToUpper() == "NAO" || input.Trim().ToUpper() == "N")
                    return false;

                return true;
            }
        }
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
            return CheckCurrencyInput("Digite o saldo inicial desta conta: R$", "Sando inicial inválido!");
        }
        public static BankAccount? GetBankAccountByAccountNumber(int? mode)
        {
            try
            {
                if(Accounts.Count == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não existem contas cadastradas neste banco.");
                    Console.ResetColor();

                    Console.WriteLine("\nPressione enter para sair...");
                    Console.ReadLine();

                    return null;
                }

                if (mode == 0)
                {
                    Console.Write("Digite o número da conta de origem: ");
                }
                else if (mode == 1)
                {
                    Console.Write("\nDigite o número da conta de destino: ");
                }
                else
                {
                    Console.Write("Digite o número da conta: ");
                }

                string? accountNumber = Console.ReadLine();

                if (accountNumber == null)
                    throw new Exception();

                BankAccount filteredAccount = Accounts.First(account => account.AccNumber == accountNumber.Trim());

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

                BankAccount filteredAccount = Accounts.First(account => account.AccNumber == accountNumber.Trim());

                if (filteredAccount == null)
                {
                    return null;
                }

                return filteredAccount;
            }
        }
        public static string? ListAllAccounts()
        {
            if (Accounts.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não existem contas cadastradas neste banco.");
                Console.ResetColor();

                Console.WriteLine("\nPressione enter para sair...");
                Console.ReadLine();

                return null;
            }

            var accountsReport = new System.Text.StringBuilder();

            foreach(var account in Accounts)
            {
                accountsReport.AppendLine($"{account.AccNumber} - Titular: {account.Name} | CPF: {account.CPF} | {account.Type} | Agência: {account.Branch}");
            }

            return accountsReport.ToString();
        }

        public string? Name { get; set; }
        public string? CPF { get; }
        public string? Address { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public string? AccNumber { get; private set; }
        public string? Branch { get; set; }
        public decimal? Balance { get; protected set; }
        public string? Type { get; set; }

        public BankAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type)
        {
            Name = name;
            CPF = cpf;
            Address = address;
            MonthlyIncome = monthlyIncome;
            Balance = 0;

            AccNumber = accountNumberSeed.ToString();
            accountNumberSeed++;

            Branch = branch;
            Type = type;

            MakeDeposit(initialBalance, Program.systemTime, DateTime.Now, TransactionType.Depósito, "Saldo inicial.");
        }
        public virtual bool MakeWithdrawal(decimal? amount, DateTime date, DateTime time, TransactionType type, string? note)
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

            if (Balance - amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. Você não possui fundos suficientes!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            Balance -= amount;
            var withdrawal = new Transaction(this, type, -amount, date.ToString("dd/MM/yyyy"), time.ToString("HH:mm:ss"), note);
            Transactions.Add(withdrawal);

            return true;
        }
        public void MakeDeposit(decimal? amount, DateTime date, DateTime time, TransactionType type, string? note)
        {
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão foi possível realizar o saque. A quantia para depósito deve ser positiva!!");
                Console.ResetColor();
                return;
            }
            Balance += amount;
            var deposit = new Transaction(this, type, amount, date.ToString("dd/MM/yyyy"), time.ToString("HH:mm:ss"), note);
            Transactions.Add(deposit);
        }
        public string ListAccountHistory()
        {
            IEnumerable<Transaction>? transactions = Transactions.Where(transaction => transaction.Account.AccNumber == AccNumber);
            var historyReport = new System.Text.StringBuilder();

            historyReport.AppendLine($"\nTitular: {Name}");
            historyReport.AppendLine($"CPF: {CPF}");
            historyReport.AppendLine($"{Type}: {AccNumber}");
            historyReport.AppendLine($"Agência: {Branch}\n");

            foreach (var transaction in transactions)
            {
                historyReport.AppendLine($"{transaction.Date} {transaction.Time} | {transaction.Type} | {String.Format("{0:#,0.00}", transaction.Amount)} | {transaction.Note}");
            }

            historyReport.AppendLine($"\nO saldo atual é de R${String.Format("{0:#,0.00}", Balance)}.");

            return historyReport.ToString();
        }
        public bool MakeTransferTo(BankAccount destination, decimal? amount)
        {
            if (!MakeWithdrawal(amount, Program.systemTime, DateTime.Now, TransactionType.Transferência, $"Transferência para {destination.Name} ({destination.Type}: {destination.AccNumber})."))
                return false;

            destination.MakeDeposit(amount, Program.systemTime, DateTime.Now, TransactionType.Transferência, $"Transferência recebida de {Name} ({Type}: {AccNumber}).");
            return true;
        }
        public static bool ListTransfers()
        {
            if(Transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAinda não existem transações neste banco.\n");
                Console.ResetColor();

                Console.WriteLine("\nPressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            foreach (Transaction transaction in Transactions)
                Console.WriteLine($"{transaction.Date} {transaction.Time} | {transaction.Type} | {transaction.Account.Type} {transaction.Account.AccNumber} - {transaction.Account.Name} - CPF: {transaction.Account.CPF} | R${String.Format("{0:#,0.00}", transaction.Amount)} | {transaction.Note}");

            return true;
        }
        public static bool ListNegativeAccounts()
        {
            Console.Clear();

            Console.WriteLine("Contas Negativas:\n");

            IEnumerable<BankAccount> negativeAccounts = Accounts.Where(account => account.Balance < 0);
            
            if(!negativeAccounts.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão existem contas negativas neste banco.");
                Console.ResetColor();

                Console.WriteLine("\nPressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var account in negativeAccounts)
            {
                Console.WriteLine($"{account.AccNumber} | {account.Name} - CPF: {account.CPF} | {account.Type} | Agência: {account.Branch} | Saldo: {account.Balance}");
            }

            Console.ResetColor();

            return true;
        }
        public void EditAccount()
        {
            int accountIndex = Accounts.FindIndex(account => account.AccNumber == AccNumber);

            Accounts[accountIndex] = this;
        }
        public bool DeleteAccount()
        {
            if (!YesOrNoAnswer("ATENÇÃO! Deseja realmente excluir esta conta"))
                return false;

            BankAccount filteredAccount = Accounts.First(account => account.AccNumber == AccNumber);
            Accounts.Remove(filteredAccount);

            return true;
        }
        public virtual void PerformSixMonthsAdvance()
        {

        }
    }
             
}
