using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.Models;
using System.Data.Entity;
using AppModel;

namespace Book.API_Controllers
{
    public class booksController : ApiController
    {
        BookDB ctx;

        public booksController()
        {
            ctx = new BookDB();
        }

        [HttpGet]
        public List<BookModel> GetBookList()
        {
            return ctx.book.Select(x => new BookModel() { BookId = x.BookId, Author = x.Author, ISBN = x.ISBN, Language = x.Language, Name = x.Name }).ToList();
        }

        [HttpGet]
        public List<BookModel> SearchbookBy([FromUri] string bookName, [FromUri] string language, [FromUri] string isbn)
        {
            return ctx.book.Where(x => x.Name.Equals(bookName) || x.Language.Equals(language) || x.ISBN.Equals(isbn)).Select(x => new BookModel() { BookId = x.BookId, Author = x.Author, ISBN = x.ISBN, Language = x.Language, Name = x.Name }).ToList();
        }

        [HttpGet]
        public BookModel GetBookByIsbn([FromUri] string isbn)
        {
            return ctx.book.Where(x => x.ISBN.Equals(isbn)).Select(x => new BookModel() { BookId = x.BookId, Author = x.Author, ISBN = x.ISBN, Language = x.Language, Name = x.Name }).FirstOrDefault();
        }

        [HttpPost]
        public HttpResponseMessage CreateBook(BookModel model)
        {
            try
            {
                var entityObj = new tblbook() { Author = model.Author, BookId = model.BookId, ISBN = model.ISBN, Language = model.Language, Name = model.Name };

                ctx.Entry(entityObj).State = EntityState.Added;

                ctx.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
    }


    public class BookDB : DbContext
    {
        public BookDB() : base("DefaultConnection")
        {
        }

        public DbSet<tblbook> book { get; set; }
    }
}
