using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand : BookStoreDbContextBase
    {
        public CreateAuthorModel Model { get; set; }
        public CreateAuthorCommand(BookStoreDbContext context,IMapper mapper):base(context,mapper){}

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s => s.Name == Model.Name && s.Surname == Model.Surname);
            if (author is not null) throw new InvalidOperationException("Author is already exists.");

            author = _mapper.Map<Author>(Model);

            _dbContext.Authors.Add(author);
        }
        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }

   
}
