using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly BookStoreDbContext _dbContext;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == GenreId);
            if (genre is null) throw new InvalidOperationException("Genre was not found.");

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        } 
    }
}
