using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Models
{
    public class Book
    {
        //Book data properties
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public ushort Year { get; set; }
        //Book status properties
        [Required]
        public bool Borrowed { get; set; }
        [MaxLength(30)]
        public string BorrowerFirstName { get; set; }
        [MaxLength(30)]
        public string BorrowerLastName { get; set; }
        public DateTime? ReturnUntil { get; set; }

        public override string ToString()
        {
            return $"{Code}-{Title}({Year})";
        }
    }
}
