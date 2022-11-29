using System.Diagnostics;
using LibraryApp.Models;
using LibraryDbApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Error { get; set; } = default!;
    public IList<BookModel> Books { get; set; } = default!;
    public IList<CustomerModel> Customers { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchTitle { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchAuthor { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string BookAction { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string BookIdCheckedOut { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string BookCheckedOutBy { get; set; } = default!;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        
        var sql = @"
            SELECT b.id, b.title, b.author, b.genre, c.name, MAX(co.due_date)
            FROM books b
            LEFT JOIN checkouts co ON co.book_id = b.id  
            LEFT JOIN customers c ON co.customer_id = c.id
            GROUP BY  b.id, b.title, b.author, b.genre, c.name";

        if (SearchTitle != null || SearchAuthor != null)
        {
            string where;
            if (SearchTitle != null && SearchAuthor != null)
            {
                where = $" HAVING b.title LIKE '%{SearchTitle}%' AND b.author LIKE '%{SearchAuthor}%'";
            }
            else if (SearchTitle != null)
            {
                where = $" HAVING b.title LIKE '%{SearchTitle}%'";
            }
            else
            {
                where = $" HAVING b.author LIKE '%{SearchAuthor}%'";
            }
            sql += where;
        }

        Books = DbService.GetBooks(sql);
        Customers = DbService.GetCustomers();
    }

    public void OnPost()
    {
        try
        {
            if(BookAction.Equals("Check Out"))
            {
                DbService.BookCheckOut(BookCheckedOutBy,BookIdCheckedOut);
            }
            else
            {
                DbService.BookCheckIn(BookIdCheckedOut);
            }
        }
        catch (Exception e)
        {
            Error = $"Failed with {BookAction}";
            _logger.LogError($"An error occured ({e.Message})");
        }
        finally
        {
            OnGet();
            RedirectToAction("Index");
        }
    }
}

