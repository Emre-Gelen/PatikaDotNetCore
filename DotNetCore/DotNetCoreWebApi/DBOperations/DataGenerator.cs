using DotNetCoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotNetCoreWebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book()
                    {
                        Title = "Lean Startup",
                        GenreId = 1, //Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12),
                        AuthorId = 1
                    },
                    new Book()
                    {
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23),
                        AuthorId = 2
                    },
                    new Book()
                    {
                        Title = "Dune",
                        GenreId = 2, //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21),
                        AuthorId = 3
                    }
                );
                context.Genres.AddRange(
                    new Genre()
                    {
                        Name = "Personal Growth"
                    },
                    new Genre()
                    {
                        Name = "Science Fiction"
                    },
                    new Genre()
                    {
                        Name = "Noval"
                    }
                );
                context.Authors.AddRange(
                    new Author()
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        BirthDate = new DateTime(1980, 10, 30),
                    },
                    new Author()
                    {
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        BirthDate = new DateTime(1972, 2, 15),
                    },
                    new Author()
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDate = new DateTime(1984, 1, 12),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}