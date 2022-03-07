﻿using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery : BookStoreDbContextBase
    {
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
        public BookDetailViewModel Hande()
        {
            var book = _dbContext.Books
                .Include(x => x.Genre)
                .Include(x => x.Author).SingleOrDefault(f => f.Id == BookId);
            if (book is null) throw new InvalidOperationException("Book was not found.");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); /*new BookDetailViewModel()*/
            return vm;
        }
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string GenreName { get; set; }
        public string AuthorName { get; set; }
    }
}
