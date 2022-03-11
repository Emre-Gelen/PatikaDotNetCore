using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.TokenOperations;
using DotNetCoreWebApi.TokenOperations.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand : BookStoreDbContextBase
    {
        public CreateTokenModel Model { get; set; }

        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper,IConfiguration configuration) : base(dbContext, mapper){
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(user => user.Email == Model.Email && user.Password == Model.Password);
            if (user is null) throw new InvalidOperationException("User not exist.");

            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccesToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _dbContext.SaveChanges();
            return token;
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
