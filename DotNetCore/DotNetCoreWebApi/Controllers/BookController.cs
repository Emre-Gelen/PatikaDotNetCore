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
        private static List<Book> BookList = new List<Book>()
        {
            new Book(){
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, //Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)
            },
            new Book(){
                Id = 2,
                Title = "Herland",
                GenreId = 2, //Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010,05,23)
            },
            new Book(){
                Id = 3,
                Title = "Dune",
                GenreId = 2, //Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2001,12,21)
            }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            return BookList.OrderBy(o => o.Id).ToList();
        }

        [HttpGet("{Id}")]
        public Book GetBookById(int Id)
        {
            var book = BookList.Find(f => f.Id == Id);
            return book != null ? book : new Book();
        }

        //[HttpGet]
        //public Book GetBookByIdFromQuery([FromQuery] int Id)
        //{
        //    var book = BookList.Find(f => f.Id == Id);
        //    return book != null ? book : new Book();
        //}
    }
}
