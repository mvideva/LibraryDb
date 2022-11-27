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
            if(BookAction.Equals("Check Out"))
            {
                var dueDate = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd HH:mm:ss.fff"); 
                sql = $"INSERT INTO checkouts (customer_id, book_id, due_date) VALUES ({BookCheckedOutBy},'{BookIdCheckedOut}','{dueDate}')";
            }
            else
            {
                sql = $"DELETE FROM checkouts WHERE book_id = '{BookIdCheckedOut}'";
            }

            var cmd = new MySqlCommand(sql, conn);
            var updated = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Print($"An error occured ({e.Message})");
        }
        finally
        {
            conn.Close();
            OnGet();
            RedirectToAction("Index");
        }
    }
}

