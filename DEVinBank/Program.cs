using DEVinBank.Classes;

var account1 = new BankAccount("Gabriel Beserra", "117.989.456-14", "Rua das Magnólias", 4000.00, 5000.00);
var account2 = new BankAccount("Gabrielle Caigara", "111.290.123-12", "Rua dos Alfedeiros", 6000.00, 4000.00);

Console.WriteLine($"Customer: {account1._name}\nCPF: {account1._cpf}\nAccount Number: {account1._accNumber}\nBranch: {account1._branchName}\nAddress: {account1._address}\nMonthly Income: R${account1._monthlyIncome}\nBalance: R${account1._balance}");
Console.WriteLine("\n");
Console.WriteLine($"Customer: {account2._name}\nCPF: {account2._cpf}\nAccount Number: {account2._accNumber}\nBranch: {account2._branchName}\nAddress: {account2._address}\nMonthly Income: R${account2._monthlyIncome}\nBalance: R${account2._balance}");


/*

using System;

namespace DEVinBank
{
    class Program
    {
        static void Main()
        {

        }
    }
}

*/