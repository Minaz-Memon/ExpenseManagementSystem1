using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagementSystem1.Models
{
    public partial class Transaction
    {
        [Key]
        public int TranscationId { get; set; }
        public string Payer { get; set; } = null!;

       // [ForeignKey("Payer")]
        //public virtual User PayerId { get; set; }
        public string Payee { get; set; } = null!;

       // [ForeignKey("Payee")]
       // public virtual User PayeeId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
