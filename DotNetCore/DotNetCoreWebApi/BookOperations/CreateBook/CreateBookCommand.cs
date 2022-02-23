using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(f => f.Title == Model.Title);
            if (book is not null) throw new InvalidOperationException("Book is already exist.");
            book = new Book();
            book.Title = Model.Title;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel {
            public string Title{ get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
