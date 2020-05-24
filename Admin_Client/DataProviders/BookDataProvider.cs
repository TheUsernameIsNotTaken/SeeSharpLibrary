using System;
using System.Collections.Generic;
using System.Net.Http;
using Library_Models;
using Newtonsoft.Json;

namespace Admin_Client.DataProviders
{
    public static class BookDataProvider
    {

        private static readonly string _url = LibraryDataProvider.bookUrl;

        ////Get a single data inside a database from the server by it's code.
        //public static Book GetSingleData(string code)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = client.GetAsync(_url + "/get/" + code).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var rawData = response.Content.ReadAsStringAsync().Result;
        //            var singleData = JsonConvert.DeserializeObject<Book>(rawData);
        //            return singleData;
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException(response.StatusCode.ToString());
        //        }
        //    }
        //}

        //Borrow a book from the library as a person.
        public static void BorrowBook(Book book, Person person)
        {
            book.IsAvailable = false;
            book.BorrowerId = person.Id;
            book.ReturnUntil = DateTime.Now.AddDays(Book.BORROWINGWEEKS * 7);
            book.TimesExtended = 0;
            LibraryDataProvider.UpdateData(_url, book, book.Id);
        }

        //Borrow a book from the library as a person.
        public static bool? ReturnBook(Book book, Person person, bool forced)
        {
            ReturnStatus status = Returnable(book, person);
            if (status == ReturnStatus.RETURNABLE || (status == ReturnStatus.RULEBREAK && forced) )
            {
                book.IsAvailable = true;
                book.BorrowerId = null;
                book.ReturnUntil = null;
                book.TimesExtended = null;
                LibraryDataProvider.UpdateData(_url, book, book.Id);
                return true;
            }
            else if(status == ReturnStatus.RULEBREAK && !forced)
            {
                return false;
            }
            return null;
        }

        public static ReturnStatus Returnable(Book book, Person person)
        {
            if (book != null && person != null && !book.IsAvailable && book.BorrowerId.Equals(person.Id))
            {
                if(book.ReturnUntil < DateTime.Now || book.TimesExtended > Book.MAXEXTENDTIMES)
                {
                    return ReturnStatus.RULEBREAK;
                }
                return ReturnStatus.RETURNABLE;
            }
            return ReturnStatus.INVALID;
        }

        //Search multiple existing books in the database on the server by their borrower's Id.
        public static IList<Book> SearchBooksByBorrower(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url + "/borrowed/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var books = JsonConvert.DeserializeObject<IList<Book>>(rawData);
                    return books;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
