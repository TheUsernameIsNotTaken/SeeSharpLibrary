using System;

namespace Library_Models
{
    public class Lending
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public Lending(string name, int time)
        {
            Name = name;
            Time = time;
        }
    }
}
