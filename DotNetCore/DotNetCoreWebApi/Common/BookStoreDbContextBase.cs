using AutoMapper;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Common
{
    public class BookStoreDbContextBase
    {
        protected readonly BookStoreDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BookStoreDbContextBase(BookStoreDbContext context,IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
    }
}
