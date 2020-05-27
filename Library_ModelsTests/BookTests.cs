using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Models.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void IsDateTextValidTest_Person()
        {
            // Arrange 
            //Initialize the variables that will be used during testing for better reading, modification and consistency
            var firstName = "Péter";
            var lastName = "Pálfi";
            var dateOfBirth = DateTime.Now.AddYears(-25).AddMonths(-1).AddDays(2);
            DateTime? expected = null;
            //Create the object to be tested
            Person test = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            // Act
            //Execute the method to be tested
            var actual = Book.IsDateTextValid(test.DateOfBirth.Date.ToString(), false);
            //Create a method, what supposedly gives the same result - Bad because allows only englidh caracters
            DateTime temp;
            if (DateTime.TryParse(test.DateOfBirth.ToString().Split(' ')[0], out temp))
            {
                expected = temp;
            }

            // Assert
            //Validate result by checking the balance value
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void IsDateTextValidTest_Book()
        {
            // Arrange 
            //Initialize the variables that will be used during testing for better reading, modification and consistency
            var code = "123-456-789-0-12-3";
            var author = "Almás Néni";
            var title = "Almamesék";
            var isAvailable = false;
            var borrowerId = 1234;
            var returnUntil = DateTime.Now.AddDays(5);
            byte timesExtended = 1;
            DateTime? expected = null;
            //Create the object to be tested
            Book test = new Book
            {
                Code = code,
                Author = author,
                Title = title,
                IsAvailable = isAvailable,
                BorrowerId = borrowerId,
                ReturnUntil = returnUntil,
                TimesExtended = timesExtended
            };

            // Act
            //Execute the method to be tested
            var actual = Book.IsDateTextValid(test.ReturnUntil.ToString(), true);
            //Create a method, what supposedly gives the same result - Bad because allows only englidh caracters
            DateTime temp;
            if (DateTime.TryParse(test.ReturnUntil.ToString(), out temp))
            {
                expected = temp;
            }

            // Assert
            //Validate result by checking the balance value
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actual);
        }
    }
}