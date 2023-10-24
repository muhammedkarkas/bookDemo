﻿using bookDemo.Data;
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

        [HttpPut]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")]int id, [FromBody]Book book)
        {
            //Check book?
            var entity = ApplicationContext.Books.Find(book => book.Id.Equals(id));
            if (entity is null)
                return NotFound(); //404

            if(id != book.Id)
                return BadRequest(); //400

            ApplicationContext.Books.Remove(entity);
            book.Id = entity.Id;

            ApplicationContext.Books.Add(book);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")] int id)
        {
            var entity = ApplicationContext.Books.Find(b => b.Id.Equals(id));
            if(entity is null)
                return NotFound(new 
                { 
                    statusCode = 404,
                    message = $"Book with {id} could not found."
                }); //404

            ApplicationContext.Books.Remove(entity);
            return NoContent();
        }

    }
}
