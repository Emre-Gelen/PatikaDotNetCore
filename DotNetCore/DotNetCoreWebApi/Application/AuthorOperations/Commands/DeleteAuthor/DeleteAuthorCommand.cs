using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        private BookStoreDbContext _dbContext; 

        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s => s.Id == AuthorId);
            if (author is null) throw new InvalidOperationException("Author was not found.");

            _dbContext.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
