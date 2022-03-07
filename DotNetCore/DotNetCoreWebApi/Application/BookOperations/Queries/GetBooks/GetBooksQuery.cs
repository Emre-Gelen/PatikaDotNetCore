﻿using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery : BookStoreDbContextBase
    {
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books
                .Include(x => x.Genre)
                .Include(x=>x.Author).OrderBy(o => o.Id).ToList();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList); 
            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string GenreName { get; set; }
            public string AuthorName { get; set; }
        }
    }
}
