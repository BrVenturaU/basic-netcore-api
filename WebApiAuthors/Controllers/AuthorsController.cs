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
    [Route("api/[controller]")]
    public class AuthorsController: ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/api/list")]
        public async Task<ActionResult<List<Author>>> Get()
        {
            return await _authorRepository.GetAllAuthors();
        }

        [HttpGet("first")]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<Author>> GetFirst()
        {
            return await _authorRepository.GetFirstAuthor();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> GetById([FromRoute] int id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Author>> GetByName([FromRoute] string name)
        {
            return await _authorRepository.GetAuthorByName(name);
        }

        [HttpGet("{id:int}/books")]
        public async Task<ActionResult<List<Book>>> GetAuthorBooks( [FromRoute] int id)
        {
            return await _authorRepository.GetAuthorBooks(id);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            return await _authorRepository.CreateAuthor(author);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] Author author, [FromRoute] int id)
        {
            return await _authorRepository.UpdateAuthor(author, id);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await _authorRepository.DeleteAuthor(id);

        }
    }
}
