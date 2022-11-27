using System;
using System.Diagnostics;
using LibraryApp.Models;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Services
{
    public class DbService
    {
        // As described in https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
        static string connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";

        public static IList<BookModel> GetBooks(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataReader? rdr = null;
            var books = new List<BookModel>();
            try
            {                
                conn.Open();

                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    books.Add(new BookModel()
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
            catch (Exception e)
            {
                Debug.Print($"An error occured ({e.Message})");
            }
            finally
            {
                rdr?.Close();
                conn.Close();
            }
            return books;
        }

        public static IList<CustomerModel> GetCustomers()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataReader? rdr = null;
            var customers = new List<CustomerModel>();

            try
            {
                conn.Open();

                var sql = "SELECT id, name FROM customers ORDER BY name";
                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    customers.Add(new CustomerModel()
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
            return customers;
        }

        public static IList<ChargesModel> GetCharges()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataReader? rdr = null;
            var charges = new List<ChargesModel>();

            try
            {
                conn.Open();

                var sql = @"SELECT cu.name, b.title, ch.description, ch.payment_date, ch.amount_due, s.name
                            FROM charges ch
                            LEFT JOIN customers cu ON cu.id = ch.customer_id
                            LEFT JOIN books b ON b.id = ch.book_id
                            LEFT JOIN staff s ON s.id = ch.staff_id
                            ";
                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    customers.Add(new CustomerModel()
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
            return customers;
        }
    }
}

