using AutoMapper;
using DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook;
using DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook;
using DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook;
using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using DotNetCoreWebApi.Application.BookOperations.Queries.GetBooks;
using DotNetCoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            //ValidationResult result = validator.Validate(command);
            //if (result.IsValid)
            //{
            //    command.Handle();
            //}
            //else
            //{
            //    foreach (var item in result.Errors)
            //    {
            //        Console.WriteLine("Property: " + item.PropertyName + " - Error Message: " + item.ErrorMessage);
            //    }
            //}
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = Id;
            new DeleteBookCommandValidator().ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetBookById(int Id)
        {
            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = Id;
            new GetBookDetailQueryValidator().ValidateAndThrow(getBookDetailQuery);
            return Ok(getBookDetailQuery.Hande());
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(new GetBooksQuery(_context, _mapper).Handle());
        }

        //[HttpGet]
        //public Book GetBookByIdFromQuery([FromQuery] int Id)
        //{
        //    var book = BookList.Find(f => f.Id == Id);
        //    return book != null ? book : new Book();
        //}
        [HttpPut("{Id}")]
        public IActionResult UpdateBook(int Id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = Id;
            command.Model = updatedBook;
            new UpdateBookCommandValidator().ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}