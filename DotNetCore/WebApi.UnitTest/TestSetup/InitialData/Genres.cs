using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
