using System;
using System.Collections.Generic;
using System.Linq;
using Library.Mvc.Controllers;
using Library.Mvc.Data.MockData;
using Library.Mvc.Data.Models;
using Library.Mvc.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LibraryMvc.Tests
{
    public class BookControllerTest
    {
        [Fact]
        public void IndexUnitTest()
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(i => i.GetAll()).Returns(MockData.GetTestBookItem());
            var controller = new BookController(mockRepo.Object);

            //act
            var result = controller.Index();

            //assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<List<Book>>(viewResult.ViewData.Model);

            var viewResultBooks = viewResult.ViewData.Model as List<Book>;
            Assert.Equal(5, viewResultBooks.Count);
        }

        [Theory]
        [InlineData("guid1", "guid2")]
        public void DetailsUnitTest(string validGuid, string invalidGuid)
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            var validItemGuid = new Guid(validGuid);
            mockRepo.Setup(i => i.GetById(validItemGuid))
                .Returns(MockData.GetTestBookItem().FirstOrDefault(i => i.Id == validItemGuid));
            var controller = new BookController(mockRepo.Object);

            //act
            var result = controller.Details(validItemGuid);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultValue = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal("Title", viewResultValue.Title);
            Assert.Equal("Author", viewResultValue.Author);
            Assert.Equal("Description", viewResultValue.Description);

            //arrange
            var invalidItemGuid = new Guid(invalidGuid);
            mockRepo.Setup(i => i.GetById(invalidItemGuid))
                .Returns(MockData.GetTestBookItem().FirstOrDefault(book => book.Id == invalidItemGuid));

            //act
            var notFoundResult = controller.Details(invalidItemGuid);

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void CreateTest()
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            var controller = new BookController(mockRepo.Object);
            var newValidItem = new Book()
            {
                Author = "Author",
                Description = "Description",
                Title = "Title"
            };

            //act
            var result = controller.Create(newValidItem);

            //assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);

            //arrange
            var newInvalidItem = new Book()
            {
                Description = "Description",
                Title = "Title"
            };
            controller.ModelState.AddModelError("Author", "The Author value is required");

            //act
            var resultInvalid = controller.Create(newInvalidItem);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultInvalid);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Theory]
        [InlineData("guid1")]
        public void DeleteTest(string validGuid)
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(i => i.GetAll()).Returns(MockData.GetTestBookItem());
            var controller = new BookController(mockRepo.Object);
            var itemGuid = new Guid(validGuid);

            //act
            var result = controller.Delete(itemGuid, null);

            //assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Null(actionResult.ControllerName);
        }
    }
}