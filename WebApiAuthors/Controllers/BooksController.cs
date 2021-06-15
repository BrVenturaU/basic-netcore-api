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
    [Route("[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return ApiResponse.NotFound("The book doesn't exists.");

            return ApiResponse.Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Book book)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                return ApiResponse.NotFound("The book doesn't exists.");

            _context.Add(book);
            await _context.SaveChangesAsync();

            return ApiResponse.Ok(message: "Book is successfully created!");
        }

    }
}
