using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Classes
{
    public class Transaction
    {
        public double _amount { get; }
        public DateTime _date { get; }
        public string _note { get; }

        public Transaction(double amount, DateTime date, string note)
        {
            _amount = amount;
            _date = date;
            _note = note;
        }
    }
}
