using AutoMapper;
using DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using DotNetCoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres;
using DotNetCoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = newGenre;

            new CreateGenreCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteGenre(int Id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = Id;

            new DeleteGenreCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetGenreDetail(int Id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = Id;

            new GetGenreDetailQueryValidator().ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            return Ok(new GetGenresQuery(_context, _mapper).Handle());
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateGenre(int Id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = Id;
            command.Model = updatedGenre;

            new UpdateGenreCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}