using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api;


namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class BookController : ControllerBase
    {
        // GET: api/<BookController>
        [HttpGet(Name = "GetAllBooks")]
        public async Task<List<Book>> Get()
        {
            Database db = new();
            return await db.GetAllBooksAsync();
        }

        // GET api/<BookController>/ID
        [HttpGet("{id}", Name ="GetOneBook")]
        public async Task<Book> GetAsync(int id)
        {
            Database db = new();
            Book book = await db.GetBookAsync(id);
            // if(BookController.ID == null){
            //     return NotFound();
            // }
            return book;
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task Post([FromBody] Book myNewBook)
        {
            Database db = new();
            db.InsertBookAsync(myNewBook);
            System.Console.WriteLine(myNewBook.Title);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}", Name="UpdateBook")]
        public async Task Put(int id, [FromBody] Book updatedBook)
        {
            Database db = new();
            await db.UpdateBookAsync(updatedBook);

        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Database db = new();
            db.DeleteBookAsync(id);
        }
    }
}
