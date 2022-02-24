using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(o => o.Id).ToList();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList); /*bookList.Select(x=> new BooksViewModel { 
                Title = x.Title,
                PageCount = x.PageCount,
                PublishDate = x.PublishDate.Date.ToString("MM-dd-yyyy"),
                GenreName = ((GenreEnum)x.GenreId).ToString()
            }).ToList();*/
            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string GenreName { get; set; }
        }
    }
}
