using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand : BookStoreDbContextBase
    {
        public CreateBookModel Model { get; set; }
        public CreateBookCommand(IBookStoreDbContext dbContext,IMapper mapper) : base(dbContext,mapper){}
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(f => f.Title == Model.Title);
            if (book is not null) throw new InvalidOperationException("Book is already exist.");
            book = _mapper.Map<Book>(Model);  //new Book();
            //book.Title = Model.Title;
            //book.PageCount = Model.PageCount;
            //book.PublishDate = Model.PublishDate;
            //book.GenreId = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel {
            public string Title{ get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
