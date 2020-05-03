using System;

namespace Library_Models
{
    class Book
    {
        //Book data properties
        private int Code { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        //Book status properties
        public bool Available { get; set; }
        public Lending LendData { get; set; }

        //Create a single new book
        public Book(int code, int isbn, string title)
        {
            Code = code;
            ISBN = isbn;
            Title = title;
            Available = true;
            LendData = null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
