using System;
using System.Collections.Generic;
using Library.Mvc.Data.Models;

namespace Library.Mvc.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book newBook);
        Book GetById(Guid id);
        void Remove(Guid id);
    }
}