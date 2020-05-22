using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Models
{
    /// <summary>
    /// Class <c>ArchiveData</c> models a book's borrowing history.
    /// It contain's the book's adn the borrowing person's data with their IDs.
    /// </summary>
    /// <remarks>
    /// The class is created to archive these data with EF Core.
    /// </remarks>
    public class ArchiveData
    {
        /// <value>
        /// Property <c>Id</c> represents the Archive Data's ID and it's key in the database.
        /// </value>
        [Key]
        public long Id { get; set; }
        /// <value>
        /// Property <c>BorrowedAt</c> contains the date of borrowing.
        /// It is required in every modell.
        /// </value>
        [Required]
        public DateTime? BorrowedAt { get; set; }
        /// <value>
        /// Property <c>ReturnedAt</c> contains the date of return.
        /// </value>
        public DateTime? ReturnedAt { get; set; }
        /// <value>
        /// Property <c>BookId</c> contains the borrowed Book's ID.
        /// It stored to support a connection with the current database.
        /// It is required in every modell.
        /// </value>
        [Required]
        public long? BookId { get; set; }
        /// <value>
        /// Property <c>Code</c> represents the borrowed Book's ISBN or other type of identifier code.
        /// Stored for backward database compatibility.
        /// It can be 20 caracters at max.
        /// It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        /// <value>
        /// Property <c>Auther</c> represents the borrowed Book's Author.
        /// Stored for backward database compatibility.
        /// It is required in every modell.
        /// </value>
        [Required]
        public string Author { get; set; }
        /// <value>
        /// Property <c>Title</c> represents the borrowed Book's Title.
        /// Stored for backward database compatibility.
        /// It is required in every modell.
        /// </value>
        [Required]
        public string Title { get; set; }
        /// <value>
        /// Property <c>BorrowerId</c> contains the borrower Person's ID.
        /// It stored to support a connection with the current database.
        /// It is required in every modell.
        /// </value>
        [Required]
        public long? BorrowerId { get; set; }
        /// <value>
        /// Property <c>FirstName</c> represents the borrower Person's first name.
        /// It can be 40 caracters at max.
        /// Stored for backward compatibility.
        /// It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        /// <value>
        /// Property <c>LastName</c> represents the borrower Person's last name.
        /// It can be 40 caracters at max.
        /// Stored for backward compatibility.
        /// It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        /// <value>
        /// Property <c>DateOfBirth</c> contains the borrower Person's date of birth.
        /// Stored for backward compatibility.
        /// It is required in every modell.
        /// </value>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>Reports the Person's data as a string.</summary>
        /// <returns>
        /// Theborrowed book's author, title and code AND the borrower Persons's first and last name, with the date of birth with the borrowing DateTime data in the following form:
        /// <c>LastName FirstName(DateOfBirth)</c>
        ///     <example>
        ///     Jack Whyte: The Skystone(978-963-426-205-3) - Vágner Máté(1999-06-24) -  Kölcsönözve:(2020.01.10. 17:04:32) Visszahozva:(2020.01.19. 17:00:02)
        ///     </example>
        /// </returns>
        public override string ToString()
        {
            return $"{Author}: {Title}({Code}) - {LastName} {FirstName}({DateOfBirth.Date}) - Kölcsönözve:({BorrowedAt}) Visszahozva:({ReturnedAt})";
        }
    }
}
