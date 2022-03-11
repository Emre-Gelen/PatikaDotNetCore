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

namespace DotNetCoreWebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IBookStoreDbContext _dbContext;
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(user => user.RefreshToken == RefreshToken && user.RefreshTokenExpireDate > DateTime.Now);
            if (user is null) throw new InvalidOperationException("Refresh token is not valid.");

            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccesToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _dbContext.SaveChanges();
            return token;
        }
    }
}
