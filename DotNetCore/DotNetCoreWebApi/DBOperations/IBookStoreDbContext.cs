using DotNetCoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApi.DBOperations
{
    public interface IBookStoreDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        int SaveChanges();
    }
}