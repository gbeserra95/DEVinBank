using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEVinBank;

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
            Console.WriteLine($"Bem-vindo ao DEVinBank! O que você deseja fazer hoje {Program.initialTime:dd/MM/yyyy}?\n");
            Console.WriteLine("1 - Criar uma conta");
            Console.WriteLine("2 - Realizar um saque");
            Console.WriteLine("3 - Fazer um depósito");
            Console.WriteLine("4 - Verificar saldo");
            Console.WriteLine("5 - Verificar extrato");
            Console.WriteLine("6 - Fazer uma tranferência");
            Console.WriteLine("7 - Listar contas DEVinBank");
            Console.WriteLine("8 - Editar dados do cliente");
            Console.WriteLine("9 - Excluir conta");
            Console.WriteLine("10 - Sair");
            Console.WriteLine("\n");
            Console.Write("Digite a opção desejada: ");

            try
            {
                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int option) && CheckInputRange(1, 10, option))
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
            Console.WriteLine("CRIAR UMA CONTA\n");
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
                    return 10 + option;
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
                    return 10 + option;
                }

                return -1;
            }
        }
    }
}
