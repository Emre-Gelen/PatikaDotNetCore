using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;

namespace WebApi.UnitTests.TestSetup.InitialData
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                   new Author()
                   {
                       Name = "Eric",
                       Surname = "Ries",
                       BirthDate = new DateTime(1980, 10, 30),
                   },
                   new Author()
                   {
                       Name = "Charlotte Perkins",
                       Surname = "Gilman",
                       BirthDate = new DateTime(1972, 2, 15),
                   },
                   new Author()
                   {
                       Name = "Frank",
                       Surname = "Herbert",
                       BirthDate = new DateTime(1984, 1, 12),
                   }
               );
        }
    }
}