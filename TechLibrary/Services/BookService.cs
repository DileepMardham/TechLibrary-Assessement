using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Classes;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        Task<(List<Book> list, int count)> GetBooksAsync(BookParameters bookParameters);
        Task<Book> GetBookByIdAsync(int bookid);
        Task<int> AddBookAsync(Book book);
        Task<int> DeleteBookAsync(int? bookId);
        Task UpdateBookAsync(Book book);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<(List<Book> list, int count)> GetBooksAsync(BookParameters bookParameters)
        {
            var queryable = _dataContext.Books.AsQueryable();

            var resultBooks = new List<Book>();
            var bookCount = 0;

            if (!string.IsNullOrEmpty(bookParameters.searchTerms))
            {                
                var terms = bookParameters.searchTerms.Split(" ");

                foreach (var term in terms)
                {
                   resultBooks.AddRange(queryable.Where(book => (book.Title != null && book.Title.ToLower().Contains(term))                        
                                    || (book.ShortDescr != null && book.ShortDescr.ToLower().Contains(term))).ToList<Book>());
                }
            }
            else
            {
                resultBooks = queryable.ToList<Book>();
            }

            bookCount = resultBooks.Count;
            resultBooks = resultBooks.Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
               .Take(bookParameters.PageSize).ToList<Book>();

            return (resultBooks, bookCount);
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }

        public async Task<int> AddBookAsync(Book book)
        {
            if (_dataContext != null)
            {
                await _dataContext.Books.AddAsync(book);
                await _dataContext.SaveChangesAsync();

                return book.BookId;
            }

            return 0;
        }

        public async Task<int> DeleteBookAsync(int? bookId)
        {
            int result = 0;

            if (_dataContext != null)
            {
                //Find the book for specific book id
                var book = await _dataContext.Books.FirstOrDefaultAsync(book => book.BookId == bookId);

                if (book != null)
                {
                    //Delete that book
                    _dataContext.Books.Remove(book);

                    //Commit the transaction
                    result = await _dataContext.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (_dataContext != null)
            {
                //Delete that book
                _dataContext.Books.Update(book);

                //Commit the transaction
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
