using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Library.API.Data.Models;
using Library.API.Data.Services;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = bookService.GetAll();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = bookService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            var item = bookService.Add(book);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = bookService.GetById(id);

            if (existingItem == null)
                return NotFound();

            bookService.Remove(id);
            return Ok();
        }
        
    }
}
