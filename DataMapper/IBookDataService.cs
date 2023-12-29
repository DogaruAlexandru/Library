﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IBookDataService
    {
        IList<Book> GetAllBooks();

        Book GetBookById(int id);

        void AddBook(Book Book);

        void DeleteBook(Book Book);

        void UpdateBook(Book Book);
    }
}
