using AutoMapper;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId;
        public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(w => w.IsActive && w.Id == GenreId);
            if (genre is null ) throw new InvalidOperationException("Genre was not found.");
            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
