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

        //Extend an already existing borrowing data in a database on the server if allowed.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="book">The book, which's borrow time we want to expend</param>
        /// <returns>
        ///     <list>
        ///         <listheader>
        ///             <term>Bool or Null</term>
        ///             <description>Returns a bool value representing the success.</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Null</term>
        ///             <description>If the extending not possible because the book or the borrowing doesn't exist.</description>
        ///         </item>
        ///         <item>
        ///             <term>False</term>
        ///             <description>If the extending not possible because we already reached the maximum # of extends, or because it already has a late return penalty fee.</description>
        ///         </item>
        ///         <item>
        ///             <term>True</term>
        ///             <description>If the extending was successfull.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static bool? ExtendBorrow(Book book)
        {
            if (book != null && book.BorrowerId != null){
                if (book.TimesExtended < Book.MAXEXTENDTIMES && DateTime.Now < book.ReturnUntil)
                {
                    //Add an extra week, and increment the # of extends;
                    book.ReturnUntil = book.ReturnUntil.Value.AddDays(7);
                    book.TimesExtended++;
                    //Update the data
                    using (var client = new HttpClient())
                    {
                        var rawData = JsonConvert.SerializeObject(book);
                        var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                        var response = client.PutAsync(_url + "/" + book.Id, content).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new InvalidOperationException(response.StatusCode.ToString());
                        }
                    }
                    return true;
                }
                return false;
            }
            return null;
        }

        public static ReturnStatus Returnable(Book book, Person person)
        {
            if (book != null && person != null && !book.IsAvailable && book.BorrowerId.Equals(person.Id))
            {
                if (book.ReturnUntil < DateTime.Now || book.TimesExtended > Book.MAXEXTENDTIMES)
                {
                    return ReturnStatus.RULEBREAK;
                }
                return ReturnStatus.RETURNABLE;
            }
            return ReturnStatus.INVALID;
        }
    }
}
