using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryDbApp.Pages;

public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;

    public IList<BookModel> Books { get; set; } = default!;
    public IList<CustomerModel> Customers { get; set; } = default!;
    public IList<StaffModel> Staff { get; set; } = default!;
    public IList<ChargesModel> Charges { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string CustomerId { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string BookId { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int StaffId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string AmountDue { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string Reason { get; set; } = default!;

    public TransactionsModel(ILogger<TransactionsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


