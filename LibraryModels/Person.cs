using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library_Models
{
    /// <summary>
    /// Class <c>Person</c> models the library's clients.
    /// </summary>
    /// <remarks>
    /// The class is created to store these data with EF Core.
    /// </remarks>
    public class Person
    {
        /// <value>
        /// Property <c>Id</c> represents the Person's ID in the library and it's key in the database.
        /// </value>
        [Key]
        public long Id { get; set; }
        /// <value>
        /// Property <c>FirstName</c> represents the Person's first name.
        /// It can be 30 caracters at max.
        /// It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        /// <value>
        /// Property <c>LastName</c> represents the Person's last name.
        /// It can be 30 caracters at max.
        /// It is required in every modell.
        /// </value>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        /// <value>
        /// Property <c>DateOfBirth</c> contains the Person's date of birth.
        /// It is required in every modell.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <value>
        /// Property <c>BorrowedBooks</c> contains a collection of books borrowed by this Person.
        /// </value>
        public ICollection<Book> BorrowedBooks { get; set; }

        /// <summary>Reports the Person's data as a string.</summary>
        /// <returns>
        /// The Persons's first and last name, with the date of birth in the following form:
        /// <c>LastName FirstName(DateOfBirth)</c>
        ///     <example>
        ///     Vágner Máté(1999-06-24)
        ///     </example>
        /// </returns>
        public override string ToString()
        {
            return $"{LastName} {FirstName}({DateOfBirth.Date})";
        }
    }
}
