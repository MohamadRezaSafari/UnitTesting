using System;
using System.Collections.Generic;
using System.Linq;
using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryAPI.Tests
{
    public class BookControllerTest
    {
        private BooksController booksController;
        private IBookService bookService;

        public BookControllerTest()
        {
            bookService = new BookService();
            booksController = new BooksController(bookService);
        }

        [Theory]
        [InlineData("guid1", "guid2")]
        public void RemoveBookById(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //act
            var notFoundResult = booksController.Remove(invalidGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, bookService.GetAll().Count());

            //act
            var okResult = booksController.Remove(validGuid);
            //assert
            Assert.IsType<NotFoundResult>(okResult);
            Assert.Equal(4, bookService.GetAll().Count());
        }

        [Fact]
        public void AddBook()
        {
            //arrange
            var completeBook = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };
            //act
            var createdResponse = booksController.Post(completeBook);
            //assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            //arrange
            var inCompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description"
            };
            //act
            booksController.ModelState.AddModelError("Title", "Title is a required field");
            var badResponse = booksController.Post(inCompleteBook);
            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Theory]
        [InlineData("guid1", "guid2")]
        public void GetBookById(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //act
            var okResult = booksController.Get(validGuid);
            var notFoundResult = booksController.Get(invalidGuid);

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<Book>(item?.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(validGuid, bookItem?.Id);
            Assert.Equal("Managing Oneself", bookItem?.Title);
        }

        [Fact]
        public void GetAll()
        {
            //arrange
            //act
            var result = booksController.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list?.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks?.Count);
        }
    }
}
