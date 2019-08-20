using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using ToRead_DAL.Models;
using ToRead_DAL.Models.Base;

namespace ToRead_DAL.Repos
{
    public class BookRepo : Repo<Book>
    {
        public override Book GetOne(int? id)
        {
            var book = Context.Books
                .Include(ab => ab.AuthorBooks)
                .ThenInclude(a => a.Author)
                .Include(bg => bg.BookGenres)
                .ThenInclude(g => g.Genre)
                .Include(l=>l.Location)
                .Single(b => b.Id == id);
            return book;
        }

        public Location GetLocation(Book book)
        {
            return this.GetOne(book.Id).Location;
        }

        public List<Author> GetAuthors(Book book)
        {
            return this.GetOne(book.Id).AuthorBooks.Select(ab => ab.Author).ToList();
        }

        public List<Genre> GetGenres(Book book)
        {
            return this.GetOne(book.Id).BookGenres.Select(bg => bg.Genre).ToList();
        }
    }
}
