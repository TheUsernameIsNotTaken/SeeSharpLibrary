﻿using System.Collections.Generic;
using System.Linq;
using Library_Models;

namespace WebAPI_Server.Repositories
{
    public static class ArchiveDataRepository
    {
        //Get all archived data.
        public static IList<ArchiveData> GetAll()
        {
            using(var database = new ArchiveDataContext())
            {
                var allData = database.Archive.ToList();
                return allData;
            }
        }

        //Get a specific data.
        public static ArchiveData GetSingle(long id)
        {
            using (var database = new ArchiveDataContext())
            {
                var data = database.Archive.Where(a => a.Id == id).FirstOrDefault();
                return data;
            }
        }

        //Get the most recent specific data with ids.
        public static ArchiveData GetLastByIds(long bookId, long borrowerId)
        {
            using (var database = new ArchiveDataContext())
            {
                var manyData = database.Archive.Where(a => a.BookId == bookId).Where(a => a.BorrowerId == borrowerId).ToList();
                var data = manyData.OrderByDescending(a => a.BorrowedAt).FirstOrDefault();
                return data;
            }
        }

        //Get a many data with a book id.
        public static IList<ArchiveData> GetManyByBook(long bookId)
        {
            using (var database = new ArchiveDataContext())
            {
                var manyData = database.Archive.Where(a => a.BookId == bookId).ToList();
                return manyData;
            }
        }

        //Get a many data with a person id.
        public static IList<ArchiveData> GetManyByPerson(long borrowerId)
        {
            using (var database = new ArchiveDataContext())
            {
                var manyData = database.Archive.Where(a => a.BorrowerId == borrowerId).ToList();
                return manyData;
            }
        }

        //Add a single archived data.
        public static void AddData(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Add(data);
                database.SaveChanges();
            }
        }

        //Update a single archived data when adding the return date and if needed.
        public static void UpdateData(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Update(data);
                database.SaveChanges();
            }
        }

        //Delete a single archived data from the database.
        public static void  DeleteData(ArchiveData data)
        {
            using (var database = new ArchiveDataContext())
            {
                database.Archive.Remove(data);
                database.SaveChanges();
            }
        }
    }
}
