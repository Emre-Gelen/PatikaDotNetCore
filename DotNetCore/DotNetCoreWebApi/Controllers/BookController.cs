using DotNetCoreWebApi.BookOperations.GetBooks;
using DotNetCoreWebApi.BookOperations.CreateBook;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCoreWebApi.BookOperations.CreateBook.CreateBookCommand;
using DotNetCoreWebApi.BookOperations.GetBookDetail;
using DotNetCoreWebApi.BookOperations.UpdateBook;
using static DotNetCoreWebApi.BookOperations.UpdateBook.UpdateBookCommand;
using DotNetCoreWebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(new GetBooksQuery(_context,_mapper).Handle());
        }

        [HttpGet("{Id}")]
        public IActionResult GetBookById(int Id)
        {
            try
            {
                GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context,_mapper);
                getBookDetailQuery.BookId = Id;
                new GetBookDetailQueryValidator().ValidateAndThrow(getBookDetailQuery);
                return Ok(getBookDetailQuery.Hande());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
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
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context,_mapper);
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
            }
            catch (Exception e)
            {
                return BadRequest(e.Message );
            }
           

            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateBook(int Id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = Id;
                command.Model = updatedBook;
                new UpdateBookCommandValidator().ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = Id;
                new DeleteBookCommandValidator().ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
