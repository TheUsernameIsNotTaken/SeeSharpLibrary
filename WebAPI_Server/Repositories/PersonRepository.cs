using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class PersonRepository
    {
        public static IList<Person> GetPeople()
        {
            /* //JSON:
            var appDataPath = GetAppDataPath();

            if (File.Exists(appDataPath))
            {
                var rawContent = File.ReadAllText(appDataPath);
                var people = JsonSerializer.Deserialize<IList<Person>>(rawContent);
                return people;
            }

            return new List<Person>();
            */

            //DataBase:
            using(var database = new PersonContext())
            {
                var people = database.People.ToList();
                return people;
            }
        }

        //DataBase: Get a specific person
        public static Person GetPerson(long id)
        {
            using (var database = new PersonContext())
            {
                var person = database.People.Where(p => p.Id == id).FirstOrDefault();
                return person;
            }
        }

        public static void AddPeople(IList<Person> people)
        {
            /* //JSON:
            var appDataPath = GetAppDataPath();

            var rawContent = JsonSerializer.Serialize(people);
            File.WriteAllText(appDataPath, rawContent);
            */

            //DataBase:
            using (var database = new PersonContext())
            {
                foreach(var person in people)
                {
                    AddPerson(person);
                }
            }
        }

        //DataBase:
        public static void AddPerson(Person person)
        {
            using (var database = new PersonContext())
            {
                database.People.Add(person);
                database.SaveChanges();
            }
        }

        public static void UpdatePerson(Person person)
        {
            using (var database = new PersonContext())
            {
                database.People.Update(person);
                database.SaveChanges();
            }
        }

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
