
namespace LibraryApp.Models
{
    public class Checkouts
    { 
        public int Customer_id { get; set; }
        public string? Book_id { get; set; }
        public DateTime? Due_date { get; set; }
        public DateTime? Return_date { get; set; }
    }
}

