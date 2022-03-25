﻿using System;
using DEVinBank.Entities;
using DEVinBank.Screens;

namespace DEVinBank
{
    class Program
    {
        public static DateTime initialTime = DateTime.Now;
        static private void Main()
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

                #region Withdrawal
                if (state == 2)
                {
                    try
                    {
                        BankAccount? account = BankAccount.GetBankAccountByAccountNumber(-1);
                        if (account == null)
                            throw new Exception();

                        Console.WriteLine($"\nTitular: {account.Name}");
                        Console.WriteLine($"CPF: {account.CPF}");
                        Console.WriteLine($"{account.Type}: {account.AccNumber}");
                        Console.WriteLine($"Agência: {account.Branch}");
                        Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", account.Balance)}\n");

                        decimal? amount = BankAccount.CheckCurrencyInput("Qual a quantia que você deseja sacar? R$", "Quantia inválida");
                        if (amount == null)
                            throw new Exception();

                        Console.Write("Adicione uma descrição para essa transação: ");
                        string? note = Console.ReadLine();

                        account.MakeWithdrawal(amount, DateTime.Now, note);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nSaque realizado com sucesso!");
                        Console.ResetColor();
                        Console.WriteLine($"Seu saldo é de R${String.Format("{0:0.00}", account.Balance)}\n");

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

                #region Deposit
                if (state == 3)
                {
                    try
                    {
                        BankAccount? account = BankAccount.GetBankAccountByAccountNumber(-1);
                        if (account == null)
                            throw new Exception();

                        Console.WriteLine($"\nTitular: {account.Name}");
                        Console.WriteLine($"CPF: {account.CPF}");
                        Console.WriteLine($"{account.Type}: {account.AccNumber}");
                        Console.WriteLine($"Agência: {account.Branch}");
                        Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", account.Balance)}\n");

                        decimal? amount = BankAccount.CheckCurrencyInput("Qual a quantia que você deseja depositar? R$", "Quantia inválida");
                        if (amount == null)
                            throw new Exception();

                        Console.Write("Adicione uma descrição para essa transação: ");
                        string? note = Console.ReadLine();

                        account.MakeDeposit(amount, DateTime.Now, note);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nDepósito realizado com sucesso!");
                        Console.ResetColor();
                        Console.WriteLine($"Seu saldo é de R${String.Format("{0:0.00}", account.Balance)}\n");

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

                #region Display Account funds
                if (state == 4)
                {
                    try
                    {
                        BankAccount? account = BankAccount.GetBankAccountByAccountNumber(-1);
                        if (account == null)
                            throw new Exception();

                        Console.WriteLine($"\nTitular: {account.Name}");
                        Console.WriteLine($"CPF: {account.CPF}");
                        Console.WriteLine($"{account.Type}: {account.AccNumber}");
                        Console.WriteLine($"Agência: {account.Branch}");
                        if (account.Balance > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", account.Balance)}\n");
                            Console.ResetColor();
                        }
                        else if (account.Balance < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", account.Balance)}\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", account.Balance)}\n");
                        }

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

                #region Display Account history
                if (state == 5)
                {
                    try
                    {
                        BankAccount? account = BankAccount.GetBankAccountByAccountNumber(-1);
                        if (account == null)
                            throw new Exception();

                        Console.WriteLine(account.ListAccountHistory());

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

                #region Transfer
                if (state == 6)
                {
                    try
                    {
                        BankAccount? originAccount = BankAccount.GetBankAccountByAccountNumber(0);
                        if (originAccount == null)
                            throw new Exception();

                        Console.WriteLine($"\nTitular: {originAccount.Name}");
                        Console.WriteLine($"CPF: {originAccount.CPF}");
                        Console.WriteLine($"{originAccount.Type}: {originAccount.AccNumber}");
                        Console.WriteLine($"Agência: {originAccount.Branch}");
                        Console.WriteLine($"Saldo: R${String.Format("{0:0.00}", originAccount.Balance)}\n");

                        BankAccount? destinationAccount = BankAccount.GetBankAccountByAccountNumber(1);
                        if (destinationAccount == null)
                            throw new Exception();

                        Console.WriteLine($"\nTitular: {destinationAccount.Name}");
                        Console.WriteLine($"CPF: {destinationAccount.CPF}");
                        Console.WriteLine($"{destinationAccount.Type}: {destinationAccount.AccNumber}");
                        Console.WriteLine($"Agência: {destinationAccount.Branch}\n");

                        decimal? amount = BankAccount.CheckCurrencyInput("Qual a quantia que você deseja transferir? R$", "Quantia inválida");
                        if (amount == null)
                            throw new Exception();

                        originAccount.MakeTransferTo(destinationAccount, amount);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nTransferência realizada com sucesso!");
                        Console.ResetColor();
                        Console.WriteLine($"Seu saldo é de R${String.Format("{0:0.00}", originAccount.Balance)}\n");

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