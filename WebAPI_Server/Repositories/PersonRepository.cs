using System.Collections.Generic;
using System.Linq;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class PersonRepository
    {
        //Get all person
        public static IList<Person> GetPeople()
        {
            using(var database = new PersonContext())
            {
                var people = database.People.ToList();
                return people;
            }
        }

        //Get a specific person
        public static Person GetPerson(long id)
        {
            using (var database = new PersonContext())
            {
                var person = database.People.Where(p => p.Id == id).FirstOrDefault();
                return person;
            }
        }

        //Get a list of persons' data by the part of their name
        public static IList<Person> SearchPersonByName(string name)
        {
            string[] names = name.Split(' ');
            using (var database = new PersonContext())
            {
                ISet<Person> found = new HashSet<Person>();
                foreach (var n in names)
                {
                    foreach (var person in database.People.Where(p => p.FirstName.Contains(n)).ToList())
                    {
                        found.Add(person);
                    }
                    foreach (var person in database.People.Where(p => p.LastName.Contains(n)).ToList())
                    {
                        found.Add(person);
                    }
                }
                return found.ToList();
            }
        }

        //Add a single person's data.
        public static void AddPerson(Person person)
        {
            using (var database = new PersonContext())
            {
                database.People.Add(person);
                database.SaveChanges();
            }
        }

        //Update a single person's data.
        public static void UpdatePerson(Person person)
        {
            using (var database = new PersonContext())
            {
                database.People.Update(person);
                database.SaveChanges();
            }
        }

        //Delete a single person's data from the database.
        public static void  DeletePerson(Person person)
        {
            using (var database = new PersonContext())
            {
                database.People.Remove(person);
                database.SaveChanges();
            }
        }
    }
}
