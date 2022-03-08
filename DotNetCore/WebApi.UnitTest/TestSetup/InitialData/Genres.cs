using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;

namespace WebApi.UnitTests.TestSetup.InitialData
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                    new Genre()
                    {
                        Name = "Personal Growth"
                    },
                    new Genre()
                    {
                        Name = "Science Fiction"
                    },
                    new Genre()
                    {
                        Name = "Noval"
                    }
                );
        }
    }
}