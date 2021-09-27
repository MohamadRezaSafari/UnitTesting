using System;
using System.Collections.Generic;
using Library.API.Data.Models;

namespace Library.API.Data.Services
{
    public class BookService : IBookService
    {
        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public Book Add(Book newBook)
        {
            throw new NotImplementedException();
        }

        public Book GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}