using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Entities;
using WebApiAuthors.Interfaces;

namespace WebApiAuthors.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<string> CreateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAuthorBooks(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetFirstAuthor()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAuthor(Author author, int id)
        {
            throw new NotImplementedException();
        }
    }
}
