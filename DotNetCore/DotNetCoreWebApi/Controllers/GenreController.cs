using AutoMapper;
using static DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres;
using DotNetCoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using DotNetCoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace DotNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            return Ok(new GetGenresQuery(_context, _mapper).Handle());
        }

        [HttpGet("{Id}")]
        public IActionResult GetGenreDetail(int Id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = Id;

            new GetGenreDetailQueryValidator().ValidateAndThrow(query);

            return Ok(query.Handle());
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
        [HttpDelete("{Id}")]
        public IActionResult DeleteGenre(int Id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = Id;

            new DeleteGenreCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
