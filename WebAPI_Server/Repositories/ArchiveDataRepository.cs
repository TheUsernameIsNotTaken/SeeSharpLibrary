using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class ArchiveDataRepository
    {
        public static IList<ArchiveData> GetPeople()
        {
            using(var database = new ArchiveDataContext())
            {
                var allData = database.Archive.ToList();
                return allData;
            }
        }

        //DataBase: Get a specific person
        public static Person GetPerson(long id)
        {
            using (var database = new ArchiveDataContext())
            {
                var person = database.People.Where(p => p.Id == id).FirstOrDefault();
                return person;
            }
        }

        public static void AddPeople(IList<ArchiveData> moreData)
        {
            foreach(var data in moreData)
            {
                AddPerson(data);
            }
        }

        //DataBase:
        public static void AddPerson(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Add(data);
                database.SaveChanges();
            }
        }

        public static void UpdatePerson(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Update(data);
                database.SaveChanges();
            }
        }

        public static void  DeletePerson(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Remove(data);
                database.SaveChanges();
            }
        }
    }
}
