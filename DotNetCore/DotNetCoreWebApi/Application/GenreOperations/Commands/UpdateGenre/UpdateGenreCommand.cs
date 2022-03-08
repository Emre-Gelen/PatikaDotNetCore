using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == GenreId);
            if (genre is null) throw new InvalidOperationException("Genre was not found.");
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name && x.Id != GenreId)) throw new InvalidOperationException("Genre is already exists");

            genre.Name = Model.Name.Trim() == string.Empty ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
