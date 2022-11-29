using System;
using System.Diagnostics;
using LibraryApp.Models;
using MySql.Data.MySqlClient;

namespace LibraryDbApp.Services
{
    public class DbService
    {
#if DEBUG
        public static string connStr = "server=localhost;port=3306;database=LibraryDb;user=lib;password=rary";
#else
        public static string connStr = "Server=MYSQL8002.site4now.net;Database=db_a908e1_mvideva;Uid=a908e1_mvideva;Pwd=Test12345678";
#endif
        public static void BookCheckOut(string bookCheckedOutBy, string bookIdCheckedOut)
        {
            var conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                var dueDate = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd HH:mm:ss.fff");
                var sql = $"INSERT INTO checkouts (customer_id, book_id, due_date) VALUES ({bookCheckedOutBy},'{bookIdCheckedOut}','{dueDate}')";
                var cmd = new MySqlCommand(sql, conn);
                var updated = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void BookCheckIn(string bookIdCheckedOut)
        {
            var connStr = DbService.connStr;
            var conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                var sql = $"DELETE FROM checkouts WHERE book_id = '{bookIdCheckedOut}'";
                var cmd = new MySqlCommand(sql, conn);
                var updated = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ChargeCustomer(string customerId, string bookId, string staffId, string amountDue, string reason)
        {
            var conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                var paymentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var sql = @$"INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description)
                VALUES ({customerId},'{bookId}',{staffId},{amountDue},'{paymentDate}','{reason}')";

                var cmd = new MySqlCommand(sql, conn);
                var inserted = cmd.ExecuteNonQuery();
                if (inserted != 1)
                {
                    throw new Exception("Failed registering the charge.");
                }
            }
            finally
            {
                conn.Close();
            }
        }

        // As described in https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
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

                var sql = @"SELECT cu.name, b.title, ch.description, ch.amount_due, ch.payment_date, s.name
                            FROM charges ch
                            LEFT JOIN customers cu ON cu.id = ch.customer_id
                            LEFT JOIN books b ON b.id = ch.book_id
                            LEFT JOIN staff s ON s.id = ch.staff_id
                            ORDER BY ch.payment_date DESC
                            ";
                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    charges.Add(new ChargesModel()
                    {
                        CustomerName = rdr[0].ToString(),
                        BookName = rdr[1].ToString(),
                        Description = rdr[2].ToString(),
                        AmountDue = rdr[3].ToString(),
                        PaymentDate = rdr[4].ToString(),
                        StaffName = rdr[5].ToString()
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
            return charges;
        }

        public static IList<StaffModel> GetStaff()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataReader? rdr = null;
            var staff = new List<StaffModel>();

            try
            {
                conn.Open();

                var sql = @"SELECT id, name FROM staff";
                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    staff.Add(new StaffModel()
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
            return staff;
        }

        public static IList<BookModel> GetBooksSimple()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataReader? rdr = null;
            var books = new List<BookModel>();
            try
            {
                conn.Open();
                var sql = "SELECT id, title FROM books";
                var cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    books.Add(new BookModel()
                    {
                        Id = rdr[0].ToString(),
                        Title = rdr[1].ToString(),
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
    }
}

