using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternUsingUOW.Core.Consts;
using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.Core.Models;

namespace RepositoryPatternUsingUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(1));
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _unitOfWork.Books.GetByIdAsync(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Books.GetAll());
        }

        //[HttpGet("GetByName")]
        //public IActionResult GetByName()
        //{
        //    return Ok(_booksRepository.FindByProperetyInclude(b => b.Title == "First Book", b=> b.Author));
        //}

        [HttpGet("GetAllMatchesOrdered")]
        public IActionResult GetAllMatchWithInclude()
        {
            return Ok(_unitOfWork.Books.FindAllOrdereWithProperetyInclude(b => b.Title.Contains("Book"),b=>b.Id, OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var book = _unitOfWork.Books.Add(new Book { Title = "Add", AuthorId = 1 });
            _unitOfWork.Complete();
            return Ok(book);
        }
    }
}
