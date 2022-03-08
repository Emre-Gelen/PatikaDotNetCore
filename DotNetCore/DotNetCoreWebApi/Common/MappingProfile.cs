using AutoMapper;
using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using DotNetCoreWebApi.Entities;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthor.GetAuthorQuery;
using static DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static DotNetCoreWebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace DotNetCoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => mf.Genre.Name))
                .ForMember(fm => fm.AuthorName, opt => opt.MapFrom(mf => mf.Author.Name + " " + mf.Author.Surname));
            CreateMap<Book, BooksViewModel>()
                .ForMember(fm => fm.GenreName, opt => opt.MapFrom(mf => mf.Genre.Name))
                .ForMember(fm => fm.AuthorName, opt => opt.MapFrom(mf => mf.Author.Name + " " + mf.Author.Surname));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>().ForMember(fm => fm.FullName, opt => opt.MapFrom(mf => mf.Name.Trim() + " " + mf.Surname.Trim()));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(fm => fm.FullName, opt => opt.MapFrom(mf => mf.Name.Trim() + " " + mf.Surname.Trim()));
        }
    }
}