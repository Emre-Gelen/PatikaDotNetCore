using AutoMapper;
using DotNetCoreWebApi.Application.UserOperations.Commands.CreateToken;
using DotNetCoreWebApi.Application.UserOperations.Commands.CreateUser;
using DotNetCoreWebApi.Application.UserOperations.Commands.RefreshToken;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.TokenOperations.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static DotNetCoreWebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static DotNetCoreWebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace DotNetCoreWebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;

            new CreateUserCommandValidator().ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
        [HttpPost("connect/token")] 
        public ActionResult<Token> CreateToken(CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context,_configuration);
            command.RefreshToken = refreshToken;
            return command.Handle();
        }
    }
}