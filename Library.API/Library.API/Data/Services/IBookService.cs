using System;
using System.Collections.Generic;
using Library.API.Data.Models;

namespace Library.API.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book newBook);
        Book GetById(Guid id);
        void Remove(Guid id);
    }
}