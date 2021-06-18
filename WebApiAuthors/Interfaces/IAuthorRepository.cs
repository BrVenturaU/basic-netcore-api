using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Interfaces
{
    public interface IAuthorRepository
    {
        Task<ActionResult<List<Author>>> GetAllAuthors();
        Task<ActionResult<Author>> GetFirstAuthor();
        Task<ActionResult<Author>> GetAuthorById(int id);
        Task<ActionResult<Author>> GetAuthorByName(string name);
        Task<ActionResult<List<Book>>> GetAuthorBooks(int authorId);
        Task<ActionResult> CreateAuthor(Author author);
        Task<ActionResult> UpdateAuthor(Author author, int id);
        Task<ActionResult> DeleteAuthor(int id);
    }
}
