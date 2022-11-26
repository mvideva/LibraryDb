namespace LibraryApp.Models
{
    public class BookModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? CheckedOutBy { get; set; }
        public string? CheckedOutUntil { get; set; }
    }
}       

