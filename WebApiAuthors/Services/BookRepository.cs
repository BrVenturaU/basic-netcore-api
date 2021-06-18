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
    public class BookRepository: IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context )
        {
            _context = context;
        }

        public async Task<ActionResult> CreateBook(Book book)
        {

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                return ApiResponse.NotFound("The book doesn't exists.");

            _context.Add(book);
            await _context.SaveChangesAsync();

            return ApiResponse.Ok(message: "Book is successfully created!");
        }

        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return ApiResponse.NotFound("The book doesn't exists.");

            return ApiResponse.Ok(book);
        }
    }
}
