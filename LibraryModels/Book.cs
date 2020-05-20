using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Models
{
    /// <summary>
    /// Class <c>Book</c> models a book owned by the library.
    /// </summary>
    /// <remarks>
    /// The class is created to store these data with EF Core.
    /// </remarks>
    public class Book
    {
        /// <summary>
        /// Constant <c>BORROWINGWEEKS</c> defines the # of weeks, that a person can borrow a book.
        /// </summary>
        public static readonly int BORROWINGWEEKS = 3;
        // Book data properties:
        /// <value>
        /// Property <c>Id</c> represents the Book's ID in the library and it's key in the database.
        /// </value>
        [Key]
        public long Id { get; set; }
        /// <value>
        /// Property <c>Code</c> represents the Book's ISBN or other type of identifier.
        /// It can be 20 caracters at max. It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        /// <value>
        /// Property <c>Auther</c> represents the Book's Author.
        /// It is required in every modell.
        /// </value>
        [Required]
        public string Author { get; set; }
        /// <value>
        /// Property <c>Title</c> represents the Book's Title.
        /// It is required in every modell.
        /// </value>
        [Required]
        public string Title { get; set; }
        /// <value>
        /// Property <c>Year</c> represents the year when the book got published.
        /// </value>
        public ushort Year { get; set; }

        //Book status properties
        /// <value>
        /// Property <c>IsAvailable</c> contains if the book is borrowed or not.
        /// It is required in every modell.
        /// </value>
        [Required]
        public bool IsAvailable { get; set; }
        /// <value>
        /// Property <c>BorrowerId</c> contains the borrower's ID.
        /// It's value is null if it is not borrowed at the moment.
        /// </value>
        public long? BorrowerId { get; set; }
        /// <value>
        /// Property <c>ReturnUntil</c> contains the expiration date of the borrowing.
        /// It's value is null if it is not borrowed at the moment.
        /// </value>
        public DateTime? ReturnUntil { get; set; }

        /// <summary>Reports the Book's data as a string.</summary>
        /// <returns>
        /// The Book's author, title and code in a string in the following form:
        /// <c>Author: Title(Code)</c>
        ///     <example>
        ///     Jack Whyte: The Skystone(978-963-426-205-3)
        ///     </example>
        /// </returns>
        public override string ToString()
        {
            return $"{Author}: {Title}({Code})";
        }
    }
}
