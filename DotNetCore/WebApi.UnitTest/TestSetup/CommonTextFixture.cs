using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.UnitTests.TestSetup.InitialData;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTextFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTextFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();

            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();

            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper(); 
        }
    }
}
