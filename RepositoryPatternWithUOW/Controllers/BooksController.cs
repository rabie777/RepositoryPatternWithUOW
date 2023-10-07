using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.BL.Consts;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.BL.UnitOfWork;
using RepositoryPatternWithUOW.core.Modles;

namespace RepositoryPatternWithUOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _UnitofWork;
        //private readonly IBaseRepository<Book> _Bookrepository;
        //public BooksController(IBaseRepository<Book> Bookrepository)
        //{
        //        _Bookrepository = Bookrepository;
        //}
        public BooksController(IUnitOfWork UnitofWork)
        {
             
            _UnitofWork = UnitofWork;
        }

        [HttpGet("{id}")]
        public IActionResult getBookById(int id)
        {
         return Ok(_UnitofWork.Books.GetById(id));
        }
        [HttpGet("Find")]
        public IActionResult FindBook()
        {
            return Ok(_UnitofWork.Books.Find(new[] { "Author" }));
        }
        [HttpGet("GetIDAsync/{id}")]
        public async Task<IActionResult> FindBookByIdAsync(int id)
        {
            return Ok(await _UnitofWork.Books.GetByIdAsync(id));
        }
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            return Ok(_UnitofWork.Books.GetAll());
        }
        [HttpGet("Find BookByTitle")]
        public IActionResult FindBookByTitle()
        {
            return Ok(_UnitofWork.Books.Find(b=>b.Title == "Magic"));
        }


        [HttpGet("FindAll")]
        public IActionResult FindAll()
        {
            return Ok(_UnitofWork.Books.FindALL(b => b.Title == "Magic", new[] {"Author"} ));
        }


        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_UnitofWork.Books.FindALL(b => b.Title == "Magic",null,null,b=>b.Title,Consts.Ascending));
        }
    }
}
