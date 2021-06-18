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

namespace WebApiAuthors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            return await _bookRepository.GetBookById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Book book)
        {
            return await _bookRepository.CreateBook(book);
        }

    }
}
