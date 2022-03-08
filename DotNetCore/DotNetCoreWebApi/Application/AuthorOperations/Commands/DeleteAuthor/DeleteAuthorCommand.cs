﻿using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        private IBookStoreDbContext _dbContext; 

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s => s.Id == AuthorId);
            var authorBooks = _dbContext.Books.Where(w => w.AuthorId == AuthorId);
            if (author is null) throw new InvalidOperationException("Author was not found.");
            if (authorBooks is not null) throw new InvalidOperationException("Author has book(s).");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
