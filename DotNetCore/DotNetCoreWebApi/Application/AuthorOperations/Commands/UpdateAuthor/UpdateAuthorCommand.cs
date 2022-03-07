using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s=>s.Id == AuthorId);
            if (author is null) throw new InvalidOperationException("Author was not found.");

            author.Name = string.IsNullOrEmpty(Model.Name) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname) ? author.Surname : Model.Surname;
            author.BirthDate = Model.BirthDate != default ? Model.BirthDate : author.BirthDate;

            _dbContext.SaveChanges();
        }
        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate{ get; set; }
        }
    }
}
