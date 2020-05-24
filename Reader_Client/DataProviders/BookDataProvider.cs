using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Reader_Client.DataProviders
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

        //TODO - ADD search by Author and Title
    }
}
