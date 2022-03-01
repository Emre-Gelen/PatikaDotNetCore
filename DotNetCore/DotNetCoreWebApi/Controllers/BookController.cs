using DotNetCoreWebApi.Application.BookOperations.Queries.GetBooks;
using DotNetCoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook;
using DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook;
using DotNetCoreWebApi.Application.BookOperations.Commands.DeleteBook;
using static DotNetCoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static DotNetCoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using DotNetCoreWebApi.DBOperations;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(new GetBooksQuery(_context, _mapper).Handle());
        }

        [HttpGet("{Id}")]
        public IActionResult GetBookById(int Id)
        {
            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = Id;
            new GetBookDetailQueryValidator().ValidateAndThrow(getBookDetailQuery);
            return Ok(getBookDetailQuery.Hande());
        }

        //[HttpGet]
        //public Book GetBookByIdFromQuery([FromQuery] int Id)
        //{
        //    var book = BookList.Find(f => f.Id == Id);
        //    return book != null ? book : new Book();
        //}

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

        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = Id;
            new DeleteBookCommandValidator().ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
