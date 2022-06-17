using Domain;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DbContext") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SqliteCreateDatabaseIfNotExists<DataContext>(modelBuilder));
        }
        public DbSet<Book> Books { get; set; }
    }
}
