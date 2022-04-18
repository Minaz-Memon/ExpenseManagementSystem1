using System;
using System.Collections.Generic;

namespace ExpenseManagementSystem1.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string Payer { get; set; } = null!;
        public string Payee { get; set; } = null!;
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
