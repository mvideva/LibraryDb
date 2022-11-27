using System.Diagnostics;
using LibraryApp.Models;
using LibraryDbApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Pages;

public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;

    public string Error { get; set; } = default!;
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
        Charges = DbService.GetCharges();
        Customers = DbService.GetCustomers();
        Books = DbService.GetBooksSimple();
        Staff = DbService.GetStaff();
    }

    public void OnPost()
    {
        if (AmountDue == null )
        {
            Error = "No amount given.";
            OnGet();
            RedirectToAction("Transactions");
            return;
        }
        var connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";
        var conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            var paymentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var sql = @$"INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description)
                VALUES ({CustomerId},'{BookId}',{StaffId},{AmountDue},'{paymentDate}','{Reason}')";

            var cmd = new MySqlCommand(sql, conn);
            var inserted = cmd.ExecuteNonQuery();
            if(inserted != 1)
            {
                Error = "Failed registering the charge.";
            }
        }
        catch (Exception e)
        {
            Error = $"Failed registering the charge due to an error";
            _logger.LogError($"An error occured ({e.Message})");
        }
        finally
        {
            conn.Close();
            OnGet();
            RedirectToAction("Transactions");
        }
    }
}


