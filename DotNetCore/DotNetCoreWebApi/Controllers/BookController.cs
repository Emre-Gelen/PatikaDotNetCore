using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        //private static List<Book> BookList = new List<Book>()
        //{
        //    new Book(){
        //        Id = 1,
        //        Title = "Lean Startup",
        //        GenreId = 1, //Personal Growth
        //        PageCount = 200,
        //        PublishDate = new DateTime(2001,06,12)
        //    },
        //    new Book(){
        //        Id = 2,
        //        Title = "Herland",
        //        GenreId = 2, //Science Fiction
        //        PageCount = 250,
        //        PublishDate = new DateTime(2010,05,23)
        //    },
        //    new Book(){
        //        Id = 3,
        //        Title = "Dune",
        //        GenreId = 2, //Science Fiction
        //        PageCount = 540,
        //        PublishDate = new DateTime(2001,12,21)
        //    }
        //};

        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Book> GetBooks()
        {
            return _context.Books.OrderBy(o => o.Id).ToList();
        }

        [HttpGet("{Id}")]
        public Book GetBookById(int Id)
        {
            var book = _context.Books.SingleOrDefault(f => f.Id == Id);
            return book != null ? book : new Book();
        }

        //[HttpGet]
        //public Book GetBookByIdFromQuery([FromQuery] int Id)
        //{
        //    var book = BookList.Find(f => f.Id == Id);
        //    return book != null ? book : new Book();
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(f => f.Title == newBook.Title);
            if (book is not null) return BadRequest();
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateBook(int Id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(f => f.Id == Id);
            if (book is null || updatedBook.Id != Id) return BadRequest();

            book.Title = string.IsNullOrEmpty(updatedBook.Title) ? book.Title : updatedBook.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            var book = _context.Books.SingleOrDefault(f => f.Id == Id);
            if (book is null) return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
