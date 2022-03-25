using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class Transaction
    {
        public string? AccNumber { get; }
        public decimal? Amount { get; }
        public DateTime Date { get; }
        public string? Note { get; }

        public Transaction(string? accountNumber, decimal? amount, DateTime date, string? note)
        {
            AccNumber = accountNumber;
            Amount = amount;
            Date = date;
            Note = note;
        }
    }
}
