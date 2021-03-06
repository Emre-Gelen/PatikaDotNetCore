using DotNetCoreWebApi.DBOperations;
using System;
using System.Linq;

namespace DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(f => f.Id == BookId);
            if (book is null) throw new InvalidOperationException("Book was not found.");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}