using System;
using DEVinBank.Classes;
using DEVinBank.Screens;

namespace DEVinBank
{
    class Program
    {
        static public void Main()
        {
            int state = 0;

            while(true)
            {
                #region When error clear console and show main menu
                if (state == -1)
                {
                    Console.Clear();
                    state = 0;
                }
                #endregion

                #region Show main menu
                if (state == 0)
                {
                    state = Menu.MainMenu();

                }
                #endregion

                #region Show create account menu
                if (state == 1)
                {
                    state = Menu.AccountMenu();
                }
                #endregion

                #region Create Checking, Savings or Investment Accounts
                if (state == 11  || state == 12 || state == 13)
                {
                    try
                    {
                        BankAccount newAccount;

                        Console.Clear();
                        var name = BankAccount.SetCustomerName();
                        if (name == null)
                            throw new Exception();

                        var cpf = BankAccount.SetCustomerCPF();
                        if (cpf == null)
                            throw new Exception();

                        var address = BankAccount.SetCustomerAddress();
                        if (address == null)
                            throw new Exception();

                        var monthlyIncome = BankAccount.SetCustomerMonthlyIncome();
                        if (monthlyIncome == null)
                            throw new Exception();

                        var branch = BankAccount.SetCustomerBranch();
                        if (branch == null)
                            throw new Exception();

                        var initialBalance = BankAccount.SetInitialBalance();
                        if (initialBalance == null)
                            throw new Exception();

                        if (state == 11)
                        {
                            string accountType = "Conta Corrente";
                            newAccount = new CheckingAccount(name, cpf, address, monthlyIncome, branch, initialBalance, accountType);
                        }
                        else if (state == 12)
                        {
                            string accountType = "Conta Poupança";
                            newAccount = new SavingsAccount(name, cpf, address, monthlyIncome, branch, initialBalance, accountType);
                        }
                        else
                        {
                            string accountType = "Conta Investimento";
                            newAccount = new InvestmentAccount(name, cpf, address, monthlyIncome, branch, initialBalance, accountType);
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Conta criada com sucesso!\n");
                        Console.ResetColor();
                        Console.WriteLine($"Titular: {newAccount.Name}");
                        Console.WriteLine($"CPF: {newAccount.CPF}");
                        Console.WriteLine($"Endereço: {newAccount.Address}");
                        Console.WriteLine($"Renda mensal: R${String.Format("{0:0.00}", newAccount.MonthlyIncome)}\n");
                        Console.WriteLine($"Tipo de conta: {newAccount.Type}");
                        Console.WriteLine($"Número da conta: {newAccount.AccNumber}");
                        Console.WriteLine($"Agência: {newAccount.Branch}");
                        Console.WriteLine($"Saldo Inicial: R${String.Format("{0:0.00}", newAccount.Balance)}\n");

                        Console.WriteLine("Pressione enter para sair...");
                        Console.ReadLine();

                        Console.Clear();
                        state = 0;
                    }
                    catch (Exception)
                    {
                        state = -1;
                    }
                }
                #endregion

                #region List all accounts
                if (state == 7)
                {
                    Console.Clear();
                    Console.WriteLine(BankAccount.ListAllAccounts());
                    Console.WriteLine("\nPressione enter para sair...");
                    Console.ReadLine();
                    Console.Clear();

                    state = 0;
                }
                #endregion

                #region Exit program
                if (state == 8)
                {
                    break;
                }
                #endregion
            }
        }
    }
}