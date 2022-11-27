namespace LibraryApp.Models
{
    public class ChargesModel
    {
        public string? CustomerName { get; set; }
        public string? BookName { get; set; }
        public string? StaffName { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Description { get; set; }
    }
}

