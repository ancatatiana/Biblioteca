using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Biblioteca
{
    class Book
    {
        #region proprietati ale clasei
        public int idBook { get; set; }
        public string TheNameOfTheBook { get;set; }
        public string AuthorName { get; set; }
        public int TheYearOfTheBook { get; set; }
        public string ISBN { get; set; }
        
        
        #endregion
        // metoda pentru salvarea carti->insert cu parametri de tip carte si conexiunea la baza de date
        public static void SaveBook(Book book, string localConn)
        {
            // cream conexiunea
            SqlConnection con = new SqlConnection(localConn);
            // cream comanda
            SqlCommand com = con.CreateCommand();
            // cream queriul de insertie
            com.CommandText = @"
                                INSERT INTO tBooks
                                    (
                                     name_of_book
                                    ,author_name
                                    ,year
                                    ,ISBN
                                    )
                            VALUES
                                   (
                                    @name_of_book,
                                    @author_name,
                                    @year,
                                    @ISBN
                                   )
                               ";
            // dam valorile pentru insertie
            com.Parameters.AddWithValue("name_of_book", book.TheNameOfTheBook ?? string.Empty);
            com.Parameters.AddWithValue("author_name", book.AuthorName ?? string.Empty);
            com.Parameters.AddWithValue("year", (object)book.TheYearOfTheBook ?? DBNull.Value);
            com.Parameters.AddWithValue("ISBN", book.ISBN ?? string.Empty);
            try
            {
                // deschidem conexiunea
                con.Open();
                //executam queriul de insert
                com.ExecuteNonQuery();
                // inchidem conexiunea
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ////in caz ca sa detectat o eroare atunci inchidem conexiunea sau poate am uitat sa inchid conexiunea
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public static void UpdateBook(Book book, string localConn)
        {
            SqlConnection con = new SqlConnection(localConn);
            SqlCommand com = con.CreateCommand();
            com.CommandText = @"
                                UPDATE tBooks
                                Set
                                name_of_book =@name_of_book,
                                author_name=@author_name,
                                year=@year,
                                ISBN=@ISBN
                                WHERE id_books=@id_books";
            com.Parameters.AddWithValue("id_books", book.idBook);
            com.Parameters.AddWithValue("name_of_book", book.TheNameOfTheBook ?? string.Empty);
            com.Parameters.AddWithValue("author_name", book.AuthorName ?? string.Empty);
            com.Parameters.AddWithValue("year", (object)book.TheYearOfTheBook ?? DBNull.Value);
            com.Parameters.AddWithValue("ISBN", book.ISBN ?? string.Empty);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }


        public static int SearchIfBookExist(Book book, string localConn)
        {
            SqlConnection con = new SqlConnection(localConn);
            SqlCommand com = con.CreateCommand();
            com.CommandText = @" SELECT id_book FROM tBooks WHERE name_of_book = @name_of_book";
            com.Parameters.AddWithValue("name_of_book", book.TheNameOfTheBook);

            try
            {
                con.Open();
                return (int)com.ExecuteScalar();
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public static List<Book> SelectBooks(string connString, string sortOrder, string sortColumn)
        {
            List<Book> listOfBooks = new List<Book>();
            SqlConnection con = new SqlConnection(connString);
            SqlCommand com = con.CreateCommand();
            com.CommandText = "SELECT * FROM tBooks ORDER BY " + sortColumn + " " + sortOrder;
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();

                        book.idBook = int.Parse(reader["id_book"].ToString());
                        book.TheNameOfTheBook = reader["name_of_book"].ToString();
                        book.AuthorName = reader["author_name"].ToString();
                        book.TheYearOfTheBook = int.Parse(reader["year"].ToString());
                        book.ISBN = reader["ISBN"].ToString();

                        listOfBooks.Add(book);
                    }
                }
                else
                {
                    listOfBooks = null;
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return listOfBooks;
        }

        public static void DeleteBook(string connString, int id_book)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand com = conn.CreateCommand();

            com.CommandText = @"DELETE FROM tBooks where id_books= @id_books";
            com.Parameters.AddWithValue("id_books", id_book);
            try
            {
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public static Book selectBook(string connString, int id_book)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand com = con.CreateCommand();

            com.CommandText = @"SELECT * FROM tBooks where id_books=@id_books";
            com.Parameters.AddWithValue("id_books", id_book);
            Book book  = new Book();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.idBook = int.Parse(reader["id_books"].ToString());
                        book.TheNameOfTheBook = reader["name_of_book"].ToString();
                        book.AuthorName = reader["author_name"].ToString();
                        book.TheYearOfTheBook = int.Parse(reader["year"].ToString());
                        book.ISBN = reader["ISBN"].ToString();
                    }
                }
                else
                {
                    book = null;
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return book;
        }
    }
}