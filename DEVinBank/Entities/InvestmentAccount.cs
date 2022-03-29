using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEVinBank.Enums;

namespace DEVinBank.Entities
{
    public class InvestmentAccount : BankAccount
    {
        protected static List<Investment> Investments = new();
        public static bool ShowInvestmentFunds()
        {
            Console.Clear();

            if (Investments.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não existem investimentos neste banco.");
                Console.ResetColor();

                Console.WriteLine("\nPressione enter para sair...");
                Console.ReadLine();

                return false;
            }

            Console.WriteLine("Fundo de Investimentos DEVinBank");

            decimal? totalInvestmentsAmount = 0;

            foreach (Investment investment in Investments)
            {
                totalInvestmentsAmount += investment.Amount;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nO valor total do Fundo de Investimentos DEVinBank é de R${String.Format("{0:#,0.00}", totalInvestmentsAmount)}.");
            Console.ResetColor();

            return true;
        }

        public static void UpdateInvestmentsAndCheckIfTheyAreReady()
        {
            try
            {
                foreach (var investment in Investments)
                {
                    double monthlyRate = Math.Pow((double)1 + Convert.ToDouble(investment.Rate) / 100, (double)1 / 12) - 1;
                    decimal? agregatedAmount = investment.Amount;

                    agregatedAmount += (agregatedAmount * Convert.ToDecimal(monthlyRate));

                    investment.Amount = agregatedAmount;

                    if(investment.FinalDate <= Program.systemTime)
                    {
                        BankAccount account = Accounts.First(account => account.AccNumber == investment.AccNumber);
                        account.MakeDeposit(agregatedAmount, investment.FinalDate, DateTime.Now, TransactionType.Investimento, $"Retorno de investimento realizado em {investment.InitialDate} com rendimento de {investment.Rate}% a.a..");
                        Investments.Remove(investment);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public InvestmentAccount(string? name, string? cpf, string? address, decimal? monthlyIncome, string? branch, decimal? initialBalance, string? type) : base(name, cpf, address, monthlyIncome, branch, initialBalance, type)
        {
            Accounts.Add(this);
        }

        public bool MakeInvestment(int state)
        {
            Console.Write("Digite a quantidade de meses que você deseja simular seu investimeto: ");

            string? monthInput = Console.ReadLine();

            if (monthInput == null)
                throw new Exception();

            int months = Convert.ToInt32(monthInput);

            if (state == 801 && months < LCI.requiredMonths)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNão é possível realizar este investimento. O tempo mínimo exigido é de {LCI.requiredMonths} meses!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();
                return false;
            }

            if (state == 802 && months < LCA.requiredMonths)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNão é possível realizar este investimento. O tempo mínimo exigido é de {LCA.requiredMonths} meses!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();
                return false;
            }

            if (state == 803 && months < CDB.requiredMonths)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNão é possível realizar este investimento. O tempo mínimo exigido é de {CDB.requiredMonths} meses!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();
                return false;
            }

            var (investmentType, yearRate, requiredMonths) = CDB;

            if (state == 801)
                (investmentType, yearRate, requiredMonths) = LCI;
                
            else if (state == 802)
                (investmentType, yearRate, requiredMonths) = LCA;

            double monthlyRate = Math.Pow((double)1 + yearRate / 100, (double)1 / 12) - 1;

            decimal? amount = CheckCurrencyInput("Qual quantia você deseja investir? R$: ", "Quantia inválida!");

            if (Balance - amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNão é possível simular este investimento. Você não possui fundos suficientes!\n");
                Console.ResetColor();

                Console.WriteLine("Pressione enter para sair...");
                Console.ReadLine();
                return false;
            }

            decimal? agregatedBalance = amount;
            for (int i = 0; i < months; i++)    
                agregatedBalance += (agregatedBalance * Convert.ToDecimal(monthlyRate));


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nATENÇÃO! Em {months} meses(s), com rentabilidade de {yearRate}% a.a., você possuirá um saldo de R${String.Format("{0:#,0.00}", Decimal.Round(Convert.ToDecimal(agregatedBalance), 2))}.");
            if (!YesOrNoAnswer("Deseja realmente fazer este investimento"))
                return false;

            string note = $"Investimento {investmentType} - {yearRate}% a.a. para retirada em {Program.systemTime.AddMonths(months):dd/MM/yyyy}.";

            MakeWithdrawal(amount, Program.systemTime, DateTime.Now, TransactionType.Investimento, note);

            Investment investment = new(AccNumber, amount, Program.systemTime, Program.systemTime.AddMonths(months), yearRate.ToString(), note);
            Investments.Add(investment);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nInvestimento realizado com sucesso. Seu saldo atual é de R${String.Format("{0:#,0.00}", Balance)}.\n");
            Console.ResetColor();

            return true;
        }
    }
}
