using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Persistence;

namespace MovieApp.IntegrationTests.InfraStructure
{
    public class TestDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder
                <ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new ApplicationDbContext(
               options);

            context.Database.EnsureCreated();


            return context;
        }
    }
}
