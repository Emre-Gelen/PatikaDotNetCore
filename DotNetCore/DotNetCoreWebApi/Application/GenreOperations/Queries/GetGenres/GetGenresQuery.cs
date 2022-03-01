using AutoMapper;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
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
