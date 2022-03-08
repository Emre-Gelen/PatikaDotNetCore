using DotNetCoreWebApi.DBOperations;
using System;
using System.Linq;

namespace DotNetCoreWebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _dbContext;

        public DeleteGenreCommand(IBookStoreDbContext context)
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