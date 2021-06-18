using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Interfaces
{
    public interface IBookRepository
    {
        Task<ActionResult<Book>> GetBookById(int id);
        Task<ActionResult> CreateBook(Book book);
    }
}
