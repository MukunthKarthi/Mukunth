using System.Collections.Generic;
using System.Web.Mvc;

namespace AppModel
{
    public class BookModel
    {        
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public List<SelectListItem> Languages { get; set; }
        public int BookId { get; set; }
    }
}
