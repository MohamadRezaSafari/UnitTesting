using System.Collections.Generic;
using Library.Mvc.Data.Models;

namespace Library.Mvc.Data.MockData
{
    public class MockData
    {
        public static IEnumerable<Book> GetTestBookItem()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    
                },
                new Book()
                {
                    
                },
            };

            return books;
        }
    }
}