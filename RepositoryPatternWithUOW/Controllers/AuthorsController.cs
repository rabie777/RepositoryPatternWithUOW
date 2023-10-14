using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.BL.UnitOfWork;
using RepositoryPatternWithUOW.core.Modles;

namespace RepositoryPatternWithUOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _repository;
        private readonly IUnitOfWork  _UnitofWork;

        //public AuthorsController(IBaseRepository<Author> repository )
        //{
        //         _repository=repository;
                
        //}
        public AuthorsController( IUnitOfWork UnitofWork)
        {
            //_repository=repository;
            _UnitofWork = UnitofWork;
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorByID(int id)
        { 
            return Ok(_UnitofWork.Authors.GetById(id));
        }
        [HttpGet("AllAuthors")]
        public IActionResult GetALLAuthors()
        {
           // return Ok(_repository.GetAll());
            return Ok(_UnitofWork.Authors.GetAll());
        }
        [HttpPost("AddFixedAuthor")]
        public IActionResult AddAuthor()
        {
          var author=  _UnitofWork.Authors.Add(new Author { Name = "Ezzat" });
            return Ok(_UnitofWork.Complete());

        }
        [HttpPost("AddAuthor")]
        public IActionResult AddAuthor(Author model)
        {
            var author = _UnitofWork.Authors.Add(model);
            return Ok(_UnitofWork.Complete());

        }

        [HttpPost("AddAuthors")]
        public IActionResult AddAuthors()
        {
            var authors = _UnitofWork.Authors.AddRange(new List<Author> { new Author { Name = "Tamer" }, new Author { Name = "Taha" } });
                _UnitofWork.Complete();
                return Ok(authors);
        }
    }
}
