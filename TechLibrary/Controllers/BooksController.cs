using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using TechLibrary.Classes;
using System.Linq;
using System;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookParameters bookParameters)
        {
            _logger.LogInformation($"Getting books based on the {bookParameters.searchTerms} in either title or description and page parameters");

            var booksData = await _bookService.GetBooksAsync(bookParameters);

            var bookResponse = _mapper.Map<List<BookResponse>>(booksData.list);

            return Ok(new { data = bookResponse, count = booksData.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bookId = await _bookService.AddBookAsync(book);
                    if (bookId > 0)
                    {
                        return Ok(bookId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] BookResponse bookResponse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var book = _mapper.Map<Book>(bookResponse);
                    await _bookService.UpdateBookAsync(book);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteBook/{bookId}")]
        public async Task<IActionResult> DeleteBook(int? bookId)
        {
            int result = 0;

            if (bookId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await _bookService.DeleteBookAsync(bookId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

       
    }
}
