using AutoMapper;
using DotNetCoreWebApi.BookOperations.GetBookDetail;
using DotNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCoreWebApi.BookOperations.CreateBook.CreateBookCommand;
using static DotNetCoreWebApi.BookOperations.GetBooks.GetBooksQuery;

namespace DotNetCoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => ((GenreEnum)mf.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => ((GenreEnum)mf.GenreId).ToString()));
        }
    }
}
