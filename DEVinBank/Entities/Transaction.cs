using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Classes
{
    public class Transaction
    {
        public decimal _amount { get; }
        public DateTime _date { get; }
        public string _note { get; }
        public decimal _currentBalance { get;}

        public Transaction(decimal amount, DateTime date, string note, decimal currentBalance)
        {
            _amount = amount;
            _date = date;
            _note = note;
            _currentBalance = currentBalance;
        }
    }
}
