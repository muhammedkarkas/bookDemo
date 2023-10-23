using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books.ToList();
            return Ok(books); //HttpStatus 200
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id)
        {
            var book = ApplicationContext.Books.SingleOrDefault(x => x.Id.Equals(id));

            if(book is null)
                return NotFound(); //404
            
            return Ok(book  ); //HttpStatus 200
        }
        //HttpGet isteklerinde Action adının önemi yoktur.Route bilgisi olarak attribute içerisinde tanımlama yapılması gerekmektedir.

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest(); //400

                ApplicationContext.Books.Add(book);
                return StatusCode(201,book); //Created
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

    }
}
