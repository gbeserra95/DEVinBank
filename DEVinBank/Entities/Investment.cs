using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Entities
{
    public class Investment
    {
        public string? AccNumber { get; }
        public decimal? Amount { get; set; }
        public DateTime InitialDate { get; }
        public DateTime FinalDate { get; }
        public string? Rate { get; }
        public string? Note { get; }

        public Investment(string? accNumber, decimal? amount, DateTime initialDate, DateTime finalDate, string? rate, string? note)
        {
            AccNumber = accNumber;
            Amount = amount;
            InitialDate = initialDate;
            FinalDate = finalDate;
            Rate = rate;
            Note = note;
        }
    }
}
