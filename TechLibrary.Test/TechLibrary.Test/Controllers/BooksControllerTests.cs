using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLibrary.Services;
using TechLibrary.Classes;
using TechLibrary.Domain;
using TechLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Controllers.Tests
{
    [TestFixture()]
    [Category("ControllerTests")]
    public class BooksControllerTests
    {

        private  Mock<ILogger<BooksController>> _mockLogger;
        private  Mock<IBookService> _mockBookService;
        private  Mock<IMapper> _mockMapper;
        private NullReferenceException _expectedException;
        private Mock<BookParameters> _mockBookParameters;
        private Mock<Book> _mockBook;
        private Mock<BookResponse> _mockBookResponse;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = new Mock<ILogger<BooksController>>();
            _mockBookService = new Mock<IBookService>();
            _mockMapper = new Mock<IMapper>();
            _mockBookParameters = new Mock<BookParameters>();
            _mockBook = new Mock<Book>();
            _mockBookResponse = new Mock<BookResponse>();
        }

        [Test()]
        public async Task GetAllTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync(_mockBookParameters.Object)).Returns(Task.FromResult(It.IsAny<(List<Book>,int)>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.GetAll(_mockBookParameters.Object);

            //  Assert
            _mockBookService.Verify(s => s.GetBooksAsync(_mockBookParameters.Object), Times.Once, "Expected GetBooksAsync to have been called once");
        }

        [Test()]
        public async Task AddBookTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.AddBookAsync(_mockBook.Object)).Returns(Task.FromResult(It.IsAny<int>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.AddBook(_mockBook.Object);

            //  Assert
            _mockBookService.Verify(s => s.AddBookAsync(_mockBook.Object), Times.Once, "Expected AddBookAsync to have been called once");
        }

        [Test()]
        public async Task UpdateBookTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.UpdateBookAsync(_mockBook.Object)).Returns(Task.FromResult(It.IsAny<OkResult>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.UpdateBook(_mockBookResponse.Object);

            //  Assert
            _mockBookService.Verify(s => s.UpdateBookAsync(_mockBook.Object), Times.Once, "Expected UpdateBookAsync to have been called once");
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test()]
        public async Task DeleteBookTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.DeleteBookAsync(It.IsAny<int?>())).Returns(Task.FromResult(It.IsAny<int>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.DeleteBook(It.IsAny<int>());

            //  Assert
            _mockBookService.Verify(s => s.DeleteBookAsync(It.IsAny<int>()), Times.Once, "Expected DeleteBookAsync to have been called once");
            Assert.IsInstanceOf<int>(result);
        }
    }
}