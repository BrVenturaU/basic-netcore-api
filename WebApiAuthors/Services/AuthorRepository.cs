using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Database;
using WebApiAuthors.Entities;
using WebApiAuthors.Interfaces;
using WebApiAuthors.Utils;

namespace WebApiAuthors.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> CreateAuthor(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return ApiResponse.Created(message: "Author is successfully created!");
        }

        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                return ApiResponse.NotFound("The author doesn't exists.");

            _context.Remove(new Author() { Id = id });
            await _context.SaveChangesAsync();

            return ApiResponse.Created(message: "Author is successfully deleted!");

        }

        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return ApiResponse.Ok(authors);
        }

        public async Task<ActionResult<List<Book>>> GetAuthorBooks(int authorId)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == authorId);
            if (!authorExists)
                return ApiResponse.NotFound("The author doesn't exists");
            var authorBooks = await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
            return ApiResponse.Ok(authorBooks);
        }

        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return ApiResponse.NotFound("The author doesn't exists");
            return ApiResponse.Ok(author);
        }

        public async Task<ActionResult<Author>> GetAuthorByName(string name)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name.Contains(name));
            if (author == null)
                return ApiResponse.NotFound("The author doesn't exists");
            return ApiResponse.Ok(author);
        }

        public async Task<ActionResult<Author>> GetFirstAuthor()
        {
            var author = await _context.Authors.FirstOrDefaultAsync();
            return ApiResponse.Ok(author);
        }

        public async Task<ActionResult> UpdateAuthor(Author author, int id)
        {
            if (author.Id != id)
                return ApiResponse.BadRequest("The author's id doesn't match.");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                return ApiResponse.NotFound("The author doesn't exists.");

            _context.Update(author);
            await _context.SaveChangesAsync();

            return ApiResponse.Ok(message: "Author is successfully updated!");
        }
    }
}
