using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorQuery : BookStoreDbContextBase
    {
        public GetAuthorQuery(IBookStoreDbContext context, IMapper mapper):base(context,mapper){}

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbContext.Authors;
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return vm;
        }
        public class AuthorsViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string FullName { get; set; }
            public DateTime BirthDate { get; set; }

        }
    }
}
