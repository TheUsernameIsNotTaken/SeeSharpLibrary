using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Library_Models.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void IsNameValidTest_Simple()
        {
            // Arrange 
            //Initialize the variables that will be used during testing for better reading, modification and consistency
            var firstName = "Peter";
            var lastName = "Palfi";
            var dateOfBirth = DateTime.Now.AddYears(-25).AddMonths(-1).AddDays(10);
            var expected = true;
            //Create the object to be tested
            Person test = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            // Act
            //Execute the method to be tested
            var actual = Person.IsValidName(test.FirstName, test.LastName);
            //Create a method, what supposedly gives the same result - Bad because allows only englidh caracters
            if (!Regex.Match(firstName, "^[A-Z][a-z]*$").Success)
            {
                expected = false;
            }
            if(!Regex.Match(lastName, "^[A-Z][a-z]*$").Success)
            {
                expected = false;
            }

            // Assert
            //Validate result by checking the balance value
            Assert.IsTrue(actual);
            Assert.AreEqual(expected, actual);
        }

        //Complex test with multiple data.
        //Data can be defined by attributes as follows
        //[DataRow(depositAmount, expected)]
        [DataRow("Péter", "Pálfi", true)]
        [DataRow("Yong", "Hu", true)]
        [DataRow("Kißlegg", "Ahmann", true)]
        [DataRow("Péter", "Petőfi", true)]
        [DataRow("Aáéíöüóúűő", "Kiss", true)]
        [DataRow("John", "Smith", true)]
        [DataRow("Aáéíöüóúűő", "Kiss", true)]
        [DataRow("A", "Bc", false)]
        [DataRow("József", "Dr. Gábori", false)]
        [DataRow("Péter Pál", "Józsefvárosi", false)]
        [DataRow("Péter", "Petőfi34", false)]
        [DataRow("József", "Péterfiaakinekolyanhosszúanevehogymártúlhosszú", false)]
        [DataRow("Aáéí.űő", "Kiss", false)]
        [DataRow("A?b", "Kiss", false)]
        [DataRow("A_-_b", "Cdef", false)]
        [DataRow("", "", false)]
        [DataRow("123", "244", false)]
        [DataRow("Péter", "NemNév", false)]
        [TestMethod] //Needs other definiton attribute than simple test
        //The test data mapped to these parameters based on the position.
        public void IsNameValidTest_DataRow(string firstName, string lastName, bool expected)
        {
            // Arrange 
            //Initialize the variables that will be used during testing for better reading, modification and consistency
            var dateOfBirth = DateTime.Now.AddYears(-25).AddMonths(-1).AddDays(-5);
            //Create the object to be tested
            Person test = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            // Act
            //Execute the method to be tested
            var actual = Person.IsValidName(test.FirstName, test.LastName);

            // Assert
            //Validate result by checking the balance value
            Assert.AreEqual(expected, actual);
        }
    }
}