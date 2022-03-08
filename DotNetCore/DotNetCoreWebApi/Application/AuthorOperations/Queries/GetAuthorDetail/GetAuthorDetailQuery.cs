using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery : BookStoreDbContextBase
    {
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper) : base(context, mapper) { }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s=>s.Id == AuthorId);
            if (author is null) throw new InvalidCastException("Author was not found.");
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
        public class AuthorDetailViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string FullName { get; set; }
            public DateTime BirthDate { get; set; }

        }
    }
}
