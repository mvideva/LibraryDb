using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Pages;

public class IndexModel : PageModel
{
    public IList<BookModel> Books { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTitle { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchAuthor { get; set; }

    public void OnGet()
    {
        // As described in https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
        var connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";
        var conn = new MySqlConnection(connStr);
        conn.Open();

        var sql = @"
        SELECT books.id, books.title, books.author, books.genre, customers.name, checkouts.due_date
        FROM((books
        INNER JOIN checkouts ON checkouts.book_id = books.id)
        INNER JOIN customers ON checkouts.customer_id = customers.id)";

        if(SearchTitle != null || SearchAuthor != null)
        {
            string where;
            if(SearchTitle != null && SearchAuthor != null)
            {
                where = $" WHERE books.title LIKE '%{SearchTitle}%' and books.author LIKE '%{SearchAuthor}%'"; 
            }
            else if(SearchTitle != null)
            {
                where = $" WHERE books.title LIKE '%{SearchTitle}%'";
            }
            else
            {
                where = $" WHERE books.author LIKE '%{SearchAuthor}%'";
            }
            sql += where;
        }

        var cmd = new MySqlCommand(sql, conn);
        var rdr = cmd.ExecuteReader();

        Books = new List<BookModel>();

        while (rdr.Read())
        {
            Books.Add(new BookModel()
            {
                Id = rdr[0].ToString(),
                Title = rdr[1].ToString(),
                Author = rdr[2].ToString(),
                Genre = rdr[3].ToString(),
                CheckedOutBy = rdr[4].ToString(),
                CheckedOutUntil = rdr[5].ToString()
            });
        }
    }
}

