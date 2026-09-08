using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieApp.Infrastructure.Persistence;

namespace MovieApp.IntegrationTests.InfraStructure
{
    public static class TestDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var connection =
                new SqliteConnection("DataSource=:memory:");

            connection.Open();

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var publisherMock = new Mock<IPublisher>();

            var context = new ApplicationDbContext(
                options,
                publisherMock.Object);

            context.Database.EnsureCreated();

            return context;
        }
    }
}