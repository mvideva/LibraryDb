using System.Diagnostics;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Pages;

public class IndexModel : PageModel
{
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

    public void OnGet()
    {
        // As described in https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
        var connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";
        var conn = new MySqlConnection(connStr);
        MySqlDataReader? rdr = null;

        try
        {
            conn.Open();
            var sql = @"
        SELECT books.id, books.title, books.author, books.genre, customers.name, customers.id, checkouts.due_date
        FROM((books
        LEFT JOIN checkouts ON checkouts.book_id = books.id)
        LEFT JOIN customers ON checkouts.customer_id = customers.id)";

            if (SearchTitle != null || SearchAuthor != null)
            {
                string where;
                if (SearchTitle != null && SearchAuthor != null)
                {
                    where = $" WHERE books.title LIKE '%{SearchTitle}%' AND books.author LIKE '%{SearchAuthor}%'";
                }
                else if (SearchTitle != null)
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
            rdr = cmd.ExecuteReader();

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

            rdr.Close();
            sql = "SELECT id, name FROM customers ORDER BY name";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            
            Customers = new List<CustomerModel>();

            while (rdr.Read())
            {
                Customers.Add(new CustomerModel()
                {
                    Id = rdr[0].ToString(),
                    Name = rdr[1].ToString(),
                });
            }
        }
        catch (Exception e)
        {
            Debug.Print($"An error occured ({e.Message})");
        }
        finally
        {
            
            rdr?.Close();
            conn.Close();
        }
    }

    public void OnPost()
    {
        var connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";
        var conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            string sql;
            var isCheckedOut = BookAction.Equals("Check Out");
            if(isCheckedOut)
            {
                var dueDate = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd HH:mm:ss.fff"); 
                sql = $"INSERT INTO checkouts (customer_id, book_id, due_date) VALUES ({BookCheckedOutBy},'{BookIdCheckedOut}','{dueDate}')";
            }
            else
            {
                sql = $"UPDATE checkouts SET return_date = '{DateTime.Now.ToString()}' WHERE book_id = '{BookIdCheckedOut}' AND return_date IS NULL";
            }

            var cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Print($"An error occured ({e.Message})");
        }
        finally
        {
            conn.Close();
            RedirectToAction("Get");
        }
    }
}

