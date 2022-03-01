using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using static DotNetCoreWebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using DotNetCoreWebApi.Entities;
using AutoMapper;

namespace DotNetCoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => mf.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => mf.Genre.Name));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
        }
    }
}
