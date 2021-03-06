using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;

namespace WebApi.UnitTests.TestSetup.InitialData
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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
        }
    }
}