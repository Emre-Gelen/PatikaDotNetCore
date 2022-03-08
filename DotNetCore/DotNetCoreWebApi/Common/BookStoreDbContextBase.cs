using AutoMapper;
using DotNetCoreWebApi.DBOperations;

namespace DotNetCoreWebApi.Common
{
    public class BookStoreDbContextBase
    {
        protected readonly IBookStoreDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BookStoreDbContextBase(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
    }
}