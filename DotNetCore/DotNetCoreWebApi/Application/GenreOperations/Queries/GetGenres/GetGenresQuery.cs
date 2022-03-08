using AutoMapper;
using DotNetCoreWebApi.DBOperations;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where(w => w.IsActive);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);
            return vm;
        }

        public class GenresViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}