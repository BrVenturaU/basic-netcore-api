using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetFirstAuthor();
        Task<Author> GetAuthorById(int id);
        Task<Author> GetAuthorByName(string name);
        Task<List<Book>> GetAuthorBooks(int authorId);
        Task<string> CreateAuthor(Author author);
        Task<string> UpdateAuthor(Author author, int id);
        Task<string> DeleteAuthor(int id);
    }
}
