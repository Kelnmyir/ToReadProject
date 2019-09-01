using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data;
using ToRead.Data.Models;
using ToRead.MVC.Models;

namespace ToRead.Services
{
    public interface IBookService
    {
        ICollection<BookModel> GetAllBooks();

        BookModel GetBook(int id);

        void Create(BookModel model);

        void Update(BookModel model);

        void Delete(BookModel model);
    }
}
