using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Database;
using WebApiAuthors.Entities;
using WebApiAuthors.Utils;

namespace WebApiAuthors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController: ControllerBase
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/api/list")]
        public async Task<ActionResult<List<Author>>> Get()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return ApiResponse.Ok(authors);
        }

        [HttpGet("first")]
        public async Task<ActionResult<Author>> GetFirst()
        {
            var author = await _context.Authors.FirstOrDefaultAsync();
            return ApiResponse.Ok(author);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return ApiResponse.NotFound("The author doesn't exists");
            return ApiResponse.Ok(author);
        }

        [HttpGet("{id:int}/books")]
        public async Task<ActionResult<List<Book>>> GetAuthorBooks(int id)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                return ApiResponse.NotFound("The author doesn't exists");
            var authorBooks = await _context.Books.Where(b => b.AuthorId == id).ToListAsync();
            return ApiResponse.Ok(authorBooks);

        }

        [HttpPost]
        public async Task<ActionResult> Post(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return ApiResponse.Created(message: "Author is successfully created!");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Author author, int id)
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                return ApiResponse.NotFound("The author doesn't exists.");

            _context.Remove(new Author() { Id = id });
            await _context.SaveChangesAsync();

            return ApiResponse.Created(message: "Author is successfully deleted!");

        }
    }
}
