using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;
using System.Linq;

namespace DotNetCoreWebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand : BookStoreDbContextBase
    {
        public CreateUserModel Model { get; set; }

        public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(f => f.Email == Model.Email);
            if (user is not null) throw new InvalidOperationException("User is already exist.");
            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}