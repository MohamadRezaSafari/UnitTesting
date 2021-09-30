using System;
using Library.Mvc.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Mvc.Data.Services;
using Microsoft.AspNetCore.Http;

namespace Library.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;


        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public ActionResult Index()
        {
            var result = bookService.GetAll();
            return View(result);
        }

        public ActionResult Details(Guid id)
        {
            var result = bookService.GetById(id);
            if (result == null)
                return NotFound();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                bookService.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            var result = bookService.GetById(id);
            return View(result);
        }

        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                bookService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}