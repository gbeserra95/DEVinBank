using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEVinBank;
using DEVinBank.Entities;

namespace DEVinBank.Screens
{
    public class Menu
    {
        public static bool CheckInputRange(int smallerOption, int greaterOption, int userInput)
        {
            if (userInput >= smallerOption && userInput <= greaterOption)
            {
                return true;
            }

            return false;

        }

        public static int MainMenu()
        {
            Console.WriteLine($"Bem-vindo ao DEVinBank! O que você deseja fazer hoje {Program.systemTime:dd/MM/yyyy}?\n");
            Console.WriteLine("1 - Criar uma conta");
            Console.WriteLine("2 - Realizar um saque");
            Console.WriteLine("3 - Fazer um depósito");
            Console.WriteLine("4 - Verificar saldo");
            Console.WriteLine("5 - Verificar extrato");
            Console.WriteLine("6 - Fazer uma tranferência");
            Console.WriteLine("7 - Simular Rentabilidade da Conta Poupança");
            Console.WriteLine("8 - Fazer um investimento");
            Console.WriteLine("9 - Listar contas DEVinBank");
            Console.WriteLine("10 - Listar todas as transferências");
            Console.WriteLine("11 - Listar contas com saldo negativo");
            Console.WriteLine("12 - Verificar valor total dos investimentos DEVinBank");
            Console.WriteLine("13 - Editar dados do cliente");
            Console.WriteLine("14 - Excluir conta");
            Console.WriteLine("15 - Adianta data do sistema em 6 meses");
            Console.WriteLine("16 - Sair");
            Console.WriteLine("\n");
            Console.Write("Digite a opção desejada: ");

            try
            {
                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 16, option))
                {
                    return option;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int AccountMenu()
        {
            Console.Clear();
            Console.WriteLine("Criar uma conta\n");
            Console.WriteLine("Qual conta você deseja criar hoje?");
            Console.WriteLine("1 - Conta corrente");
            Console.WriteLine("2 - Conta poupança");
            Console.WriteLine("3 - Conta investimento");
            Console.WriteLine("\n");
            Console.Write("Digite a opção desejada: ");

            try
            {
                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 3, option))
                {
                    return 100 + option;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOpção inválida!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 3, option))
                {
                    return 100 + option;
                }

                return -1;
            }
        }

        public static int InvestmentMenu()
        {
            Console.Clear();
            Console.WriteLine("Fazer um investimento\n");
            Console.WriteLine("Qual tipo de investimento você deseja realizar?");
            Console.WriteLine($"1 - LCI {BankAccount.LCI.rate}% a.a. - tempo mínimo de aplicação: {BankAccount.LCI.requiredMonths} meses");
            Console.WriteLine($"2 - LCA {BankAccount.LCA.rate}% a.a. - tempo mínimo de aplicação: {BankAccount.LCA.requiredMonths} meses");
            Console.WriteLine($"3 - CDB {BankAccount.CDB.rate}% a.a. - tempo mínimo de aplicação: {BankAccount.CDB.requiredMonths} meses");
            Console.WriteLine("\n");
            Console.Write("Digite a opção desejada: ");

            try
            {
                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 3, option))
                {
                    return 800 + option;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOpção inválida!");
                Console.Write("Digite novamente: ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 3, option))
                {
                    return 800 + option;
                }

                return -1;
            }
        }
    }
}
