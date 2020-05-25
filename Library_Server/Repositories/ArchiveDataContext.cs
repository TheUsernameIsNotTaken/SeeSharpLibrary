using Library_Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Server.Repositories
{
    public class ArchiveDataContext : DbContext
    {
        public DbSet<ArchiveData> Archive { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\mssqllocaldb;Database=ServerDb;Integrated Security=True;"
            );
        }
    }
}
