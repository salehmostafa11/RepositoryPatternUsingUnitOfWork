using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternUsingUOW.Core.DTOs.Authors;
using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.Core.Models;

namespace RepositoryPatternUsingUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController<Author, CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>
    {
        #region Old
        //private readonly IUnitOfWork _unitOfWork;
        //public AuthorsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //[HttpGet]
        //public IActionResult GetById()
        //{
        //    return Ok(_unitOfWork.Authors.GetById(1));
        //}

        //[HttpGet("GetByIdAsync")]
        //public async Task<IActionResult> GetByIdAsync()
        //{
        //    return Ok(await _unitOfWork.Authors.GetByIdAsync(1));
        //}

        //[HttpGet("GetAll")]
        //public IActionResult GetAll()
        //{
        //    return Ok(_unitOfWork.Authors.GetAll());
        //}
        #endregion

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        protected override IBaseRepository<Author> Repository => _unitOfWork.Authors;

    }
}
