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
        public async Task<ActionResult<List<Author>>> Get()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return ApiResponse.Ok(authors);
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
