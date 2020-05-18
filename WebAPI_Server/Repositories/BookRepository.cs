using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class BookRepository
    {
        public static IList<Book> GetBooks()
        {
            using(var database = new BookContext())
            {
                var books = database.Books.ToList();
                return books;
            }
        }

        public static Book GetBook(long id)
        {
            using (var database = new BookContext())
            {
                var book = database.Books.Where(b => b.Id == id).FirstOrDefault();
                return book;
            }
        }

        public static Book GetBookByCode(string code)
        {
            using (var database = new BookContext())
            {
                var book = database.Books.Where(b => b.Code == code).FirstOrDefault();
                return book;
            }
        }

        public static void AddBooks(IList<Book> books)
        {
            using (var database = new BookContext())
            {
                foreach(var book in books)
                {
                    AddBook(book);
                }
            }
        }

        //DataBase:
        public static void AddBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Add(book);
                database.SaveChanges();
            }
        }

        public static void UpdateBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Update(book);
                database.SaveChanges();
            }
        }

        public static void  DeleteBook(Book book)
        {
            using (var database = new BookContext())
            {
                database.Books.Remove(book);
                database.SaveChanges();
            }
        }
    }
}
