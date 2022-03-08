using DotNetCoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.DBOperations
{
    public class BookStoreDbContext : DbContext,IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Author> Authors{ get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
