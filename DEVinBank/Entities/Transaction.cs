using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class Transaction
    {
        public decimal? Amount { get; }
        public DateTime Date { get; }
        public string? Note { get; }
        public decimal? CurrentBalance { get;}

        public Transaction(decimal? amount, DateTime date, string? note, decimal? currentBalance)
        {
            Amount = amount;
            Date = date;
            Note = note;
            CurrentBalance = currentBalance;
        }
    }
}
