﻿using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookDetailViewModel Hande()
        {
            var book = _dbContext.Books.SingleOrDefault(f => f.Id == BookId);
            if (book is null) throw new InvalidOperationException("Book is not found.");
            
            BookDetailViewModel vm = new BookDetailViewModel()
            {
                Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("MM-dd-yyyy"),
                GenreName = ((GenreEnum)book.GenreId).ToString()
            };
            return vm;
        }
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string GenreName { get; set; }
    }
}
