using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Admin_Client.DataProviders
{
    public static class BookDataProvider
    {
        private static string _url = LibraryDataProvider.bookUrl;

        //Get a single data inside a database from the server by it's code.
        public static Book GetSingleData(string code)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url + "/get/" + code).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var singleData = JsonConvert.DeserializeObject<Book>(rawData);
                    return singleData;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Borrow a book from the library as a person.
        public static void BorrowBook(Book book, Person person)
        {
            book.IsAvailable = false;
            book.BorrowerId = person.Id;
            book.ReturnUntil = DateTime.Now.AddDays(Book.BORROWINGWEEKS * 7);
            LibraryDataProvider.UpdateData(_url, book, book.Id);
        }

        ////Search multiple existing books in the database on the server by part of their Code.
        //public static IList<Book> SearchBooks(string code)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = client.GetAsync(_url + "/search/" + code).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var rawData = response.Content.ReadAsStringAsync().Result;
        //            var books = JsonConvert.DeserializeObject<IList<Book>>(rawData);
        //            return books;
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException(response.StatusCode.ToString());
        //        }
        //    }
        //}
    }
}
