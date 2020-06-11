using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithMongo.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookServices _bookServices;
        public BooksController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }
        // GET: Books
        [HttpGet]
        public ActionResult  GetBooks()
        {
            var books = _bookServices.GetBooks();
            return Ok(books);
        }




    }
}