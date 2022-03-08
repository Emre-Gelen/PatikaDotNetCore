using AutoMapper;
using DotNetCoreWebApi.Common;
using DotNetCoreWebApi.DBOperations;
using DotNetCoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand : BookStoreDbContextBase
    {
        public CreateGenreModel Model { get; set; }
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper) : base(context, mapper) { }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Name == Model.Name);
            if (genre is not null) throw new InvalidOperationException("Genre is already exists.");

            genre = new Genre();
            genre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
        }
    }
}
