using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using AppModel;
using System.Net.Http.Formatting;

namespace Book.MvcController
{
    public class BookController : Controller
    {
        [HttpGet]
        public ActionResult list()
        {
            List<BookModel> bookList;

            if (TempData["BookList"] != null)
            {
                bookList = TempData["BookList"] as List<BookModel>;
            }
            else
            {
                bookList = JsonConvert.DeserializeObject<List<BookModel>>(new HttpClient().GetAsync(HttpContext.Application["WebApiUrl"].ToString() + "/books/GetBookList").Result.Content.ReadAsStringAsync().Result);
            }

            return View(bookList);
        }

        [HttpGet]
        public ActionResult search(string bookName, string language, string isbn)
        {
            var apiUrl = HttpContext.Application["WebApiUrl"].ToString() + "/books/SearchbookBy?bookName=" + bookName + "&&language=" + language + "&&isbn=" + isbn;

            var bookList = JsonConvert.DeserializeObject<List<BookModel>>(new HttpClient().GetAsync(apiUrl).Result.Content.ReadAsStringAsync().Result);

            TempData["BookList"] = bookList;

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult books(string isbn)
        {
            var apiUrl = HttpContext.Application["WebApiUrl"].ToString() + "/books/GetBookByIsbn?isbn=" + isbn;

            var bookList = JsonConvert.DeserializeObject<BookModel>(new HttpClient().GetAsync(apiUrl).Result.Content.ReadAsStringAsync().Result);

            return View();
        }

        [HttpGet]
        public ActionResult createBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createBook(BookModel model)
        {
            var result = new HttpClient().PostAsJsonAsync(HttpContext.Application["WebApiUrl"].ToString() + "/books/CreateBook", model).Result;

            return RedirectToAction("list");
        }
    }
}