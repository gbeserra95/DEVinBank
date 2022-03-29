using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEVinBank.Enums;

namespace DEVinBank.Entities
{
    public class Transaction
    {
        public BankAccount Account { get; }
        public TransactionType Type { get; }
        public decimal? Amount { get; }
        public string Date { get; }
        public string Time { get; }
        public string? Note { get; }

        public Transaction(BankAccount account, TransactionType type, decimal? amount, string date, string time, string? note)
        {
            Account = account;
            Type = type;
            Amount = amount;
            Date = date;
            Time = time;
            Note = note;
        }
    }
}
