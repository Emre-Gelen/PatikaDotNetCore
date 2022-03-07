using AutoMapper;
using DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using DotNetCoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace DotNetCoreWebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateAuthor(int Id,[FromBody] UpdateAuthorModel author)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = Id;
            command.Model = author;

            new UpdateAuthorCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = newAuthor;

            new CreateAuthorCommandValidator().ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
