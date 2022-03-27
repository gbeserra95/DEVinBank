using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class SavingsAccount : BankAccount
    {
        public static decimal? CheckRentabilityInput(string instructionMessage, string errorMessage)
        {
            Console.Write(instructionMessage);

            try
            {
                string? value = Console.ReadLine();

                if (value == null)
                    throw new Exception();

                decimal decimalValue = Convert.ToDecimal(value.Trim());

                if (decimalValue <= 0)
                    throw new Exception();

                return decimalValue;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{errorMessage}");
                Console.ResetColor();
                Console.Write("Digite novamente: ");

                string? value = Console.ReadLine();

                if (value == null)
                    return null;

                decimal decimalValue = Convert.ToDecimal(value.Trim());

                if (decimalValue <= 0)
                    return null;

                return decimalValue;
            }
        }

        public SavingsAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type) : base(name, cpf, address, monthlyIncome, branch, initialBalance, type)
        {
            Accounts.Add(this);
        }

        public bool SimulateAccountInterest()
        {
            try
            {
                Console.Write("Digite a quantidade de meses que você deseja para simular seu investimeto: ");

                string? monthInput = Console.ReadLine();

                if (monthInput == null)
                    throw new Exception();

                int months = Convert.ToInt32(monthInput);

                if (months <= 0)
                    throw new Exception();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nATENÇÃO: Para simular sua rentabilidade, você deve escolher uma porcentagem de rendimento anual.\nPor exemplo: escolha 11 para simular uma rentabilidade de 11% a.a..");
                Console.ResetColor();

                Console.Write("\nDigite a rentabilidade anual que você deseja para simular seu investimento: ");

                string? yearRate = Console.ReadLine();

                if (yearRate == null)
                    throw new Exception();

                decimal monthRate = Convert.ToDecimal(yearRate)/12;

                if (monthRate <= 0)
                    throw new Exception();

                decimal? agregatedBalance = Balance * (1 + (months * monthRate) / 100);
                                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nEm {months} meses(s), com rentabilidade de {yearRate}% a.a., você possuirá um saldo de R${String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(agregatedBalance), 2))}.\n");
                Console.ResetColor();

                return true;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNúmero inválido!");
                Console.ResetColor();
                Console.Write("Digite novamente: ");

                string? monthInput = Console.ReadLine();

                if (monthInput == null)
                    return false;

                int months = Convert.ToInt32(monthInput);

                if (months <= 0)
                    return false;

                Console.WriteLine("Digite a porcentagem da rentabilidade anual que você deseja para simular seu investimeto)");
                Console.Write("Ex: digite 11 para simular uma rentabilidade de 11% a.a.): ");

                string? rateInput = Console.ReadLine();

                if (rateInput == null)
                    return false;

                decimal rate = Convert.ToDecimal(rateInput);

                if (rate <= 0)
                    return false;

                decimal? simulatedBalance = 0;
                decimal? agregatedBalance = Balance;

                for (int i = 0; i < months; i++)
                {
                    agregatedBalance *= rate / 12;
                    simulatedBalance += agregatedBalance;
                }

                Console.WriteLine($"Em {months} meses(s), com rentabilidade de {rate}% a.a., você possuirá um saldo de R${String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(simulatedBalance), 2))}");

                return true;
            }
        }
    }
}
