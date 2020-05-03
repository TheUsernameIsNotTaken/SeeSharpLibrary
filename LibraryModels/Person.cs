using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Models
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
