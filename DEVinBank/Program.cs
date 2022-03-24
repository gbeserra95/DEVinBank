using System;
using DEVinBank.Classes;
using DEVinBank.Screens;

namespace DEVinBank
{
    class Program
    {
        static public void Main()
        {
            int state = -1;

            while(true)
            {
                #region Clear console and show main menu
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

                #region Create Checking Account
                if (state == 11)
                {
                    Console.Clear();
                    var name = BankAccount.SetCustomerName();
                    if (name == null)
                        state = -1;

                    var cpf = BankAccount.SetCustomerCPF();
                    if (cpf == null)
                        state = -1;

                    var branch = BankAccount.SetCustomerBranch();
                    if (branch == null)
                        state = -1;
                }
                #endregion

                // Exit program
                if (state == 7)
                {
                    break;
                }
            }
        }
    }
}