namespace LibraryApp.Models
{
    public class Charges
    {
        public int Customer_id { get; set; }
        public string? Book_id { get; set; }
        public int staff_id { get; set; }
        public decimal Amount_due { get; set; }
        public DateTime? Payment_date { get; set; }
        public string? Description { get; set; }
    }
}

