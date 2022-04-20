namespace ExpenseManagementSystem1.Models
{
    public class TransactionAC
    {
        public int TransactionId { get; set; }
        public string Payer { get; set; }
        public string Payee { get; set; }
        public string Name { get; set; }

        public User Email { get; set; }
        public User PhoneNumber { get; set; }

    }
}
