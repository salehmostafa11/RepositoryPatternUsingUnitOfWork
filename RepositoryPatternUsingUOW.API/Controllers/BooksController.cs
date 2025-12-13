using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternUsingUOW.Core.Consts;
using RepositoryPatternUsingUOW.Core.DTOs;
using RepositoryPatternUsingUOW.Core.DTOs.Books;
using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.Core.Models;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController<Book,CreateBook,GetBook,UpdateBook>
    {
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        protected override IBaseRepository<Book> Repository => _unitOfWork.Books;

        [HttpPost]
        public override async Task<ActionResult<GeneralResponse<CreateBook>>> Add(CreateBook dto)
        {
            if (!await _unitOfWork.Authors.exists(a => a.Id == dto.AuthorId))
            {
                return BadRequest(new GeneralResponse<CreateBook>
                {
                    Success = false,
                    Message = $"No author with id {dto.AuthorId}!",
                    Data = dto
                });
            }

            var book = _mapper.Map<Book>(dto);

            Repository.Add(book);

            var result = _unitOfWork.Complete();

            if (!(result > 0))
            {
                return BadRequest(new GeneralResponse<CreateBook>
                {
                    Success = false,
                    Message = "Failed to add book!",
                    Data = dto
                });
            }

            return Ok(new GeneralResponse<CreateBook>
            {
                Success = true,
                Message = "Book added successfully!",
                Data = dto
            });
        }
        #region Old
        //    private readonly IUnitOfWork _unitOfWork;
        //    private readonly IMapper _mapper;

        //    public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        //    {
        //        _unitOfWork = unitOfWork;
        //        _mapper = mapper;
        //    }

        //    [HttpPost]
        //    public ActionResult<GeneralResponse<CreateBook>> Add(CreateBook entity)
        //    {
        //        var mappedData = _mapper.Map<Book>(entity);
        //        _unitOfWork.Books.Add(mappedData);
        //        var result = _unitOfWork.Complete();

        //        if (!(result > 0))
        //            return Ok(new GeneralResponse<CreateBook>
        //            {
        //                Success = false,
        //                Message = "Failed to add!",
        //                Data = entity
        //            });

        //        return Ok(new GeneralResponse<CreateBook>
        //        {
        //            Success = true,
        //            Message = "Added Succesfully!",
        //            Data = entity
        //        });
        //    }

        //    [HttpGet("GetAll")]
        //    public async Task<ActionResult<GeneralResponse<IEnumerable<GetBook>>>> GetAll()
        //    {
        //        var result = await _unitOfWork.Books.GetAllIncluded(b => b.Author);

        //        var mappedData = _mapper.Map<IEnumerable<GetBook>>(result);

        //        return Ok(new GeneralResponse<IEnumerable<GetBook>> { Success = true, Message = "All books!", Data = mappedData });
        //    }

        //    [HttpGet("{id:int}")]
        //    public async Task<ActionResult<GeneralResponse<GetBook>>> GetById(int id)
        //    {
        //        var entity = await _unitOfWork.Books.GetByCriteriaIncluded(b => b.Id == id, b => b.Author);
        //        if (entity == null)
        //        {
        //            return NotFound(
        //                new GeneralResponse<GetBook>
        //                {
        //                    Success = false,
        //                    Message = $"Book with id {id} not found!"
        //                });
        //        }

        //        var mappedData = _mapper.Map<GetBook>(entity);
        //        return Ok(
        //            new GeneralResponse<GetBook>
        //            {
        //                Success = true,
        //                Message = "Book found!",
        //                Data = mappedData
        //            });
        //    }
        //    //[HttpGet("{name:alpha}")]
        //    //public async Task<ActionResult<GeneralResponse<GetBook>>> GetByName(string name)
        //    //{
        //    //    var entity = await _unitOfWork.Books.SearchByCriteriaIncluded(b => b.Title == name, b => b.Author);
        //    //    if (entity != null)
        //    //    {
        //    //        var mappedData = _mapper.Map<GetBook>(entity);
        //    //        return Ok(
        //    //            new GeneralResponse<GetBook>
        //    //            {
        //    //                Success = true,
        //    //                Message = "Book found!",
        //    //                Data = mappedData
        //    //            });
        //    //    }

        //    //    return NotFound(
        //    //        new GeneralResponse<GetBook>
        //    //        {
        //    //            Success = false,
        //    //            Message = $"Book with name {name} not found!"
        //    //        });
        //    //}


        //    [HttpGet("search/{name:alpha}")]
        //    public async Task<ActionResult<GeneralResponse<IEnumerable<GetBook>>>> Search(string name)
        //    {
        //        var result = await _unitOfWork.Books.SearchIncluded(b=>b.Title == name, b => b.Author);
        //        if(result == null)
        //        {
        //            return BadRequest(
        //                new GeneralResponse<IEnumerable<GetBook>>
        //                {
        //                    Success = false,
        //                    Message = "Connection error!"
        //                });
        //        }

        //        var mappedData = _mapper.Map<IEnumerable<GetBook>>(result);
        //        return Ok(new GeneralResponse<IEnumerable<GetBook>>
        //        {
        //            Success= true,
        //            Message = $"{mappedData.Count()} book found.",
        //            Data = mappedData
        //        });
        //    }

        //    [HttpPut("Update")]
        //    public async Task<ActionResult<GeneralResponse<UpdateBook>>> Update(UpdateBook entity)
        //    {
        //        var objFromDb = await _unitOfWork.Books.GetByCriteriaIncluded(b => b.Id == entity.Id);
        //        if (objFromDb == null)
        //            return NotFound(new GeneralResponse<UpdateBook>
        //            {
        //                Success = false,
        //                Message = "Book not found!"
        //            });

        //        if(!await _unitOfWork.Authors.exists(a=>a.Id == entity.AuthorId))
        //        {
        //            return BadRequest(new GeneralResponse<UpdateBook>
        //            {
        //                Success = false,
        //                Message = $"No author with id {entity.AuthorId}!"
        //            });
        //        }

        //        objFromDb.Title = entity.Name;
        //        objFromDb.AuthorId = entity.AuthorId;

        //        _unitOfWork.Books.Update(objFromDb);

        //        var result = _unitOfWork.Complete();
        //        if(!(result >=0))
        //            return BadRequest(new GeneralResponse<UpdateBook>
        //            {
        //                Success = false,
        //                Message = "Filed to update book details!"
        //            });

        //        return Ok(new GeneralResponse<UpdateBook>
        //        {
        //            Success = true,
        //            Message = "Updated successfuly!",
        //            Data = entity
        //        });
        //    }

        //    [HttpDelete]
        //    public async Task<ActionResult<GeneralResponse<string>>> Delete(int id)
        //    {
        //        var entity = await _unitOfWork.Books.GetByCriteriaIncluded(b => b.Id == id);

        //        if (entity == null) return NotFound(
        //            new GeneralResponse<string> {
        //                Success = false,
        //                Message = $"Book with id {id} not found!"
        //            });

        //        _unitOfWork.Books.Delete(entity);
        //        var result = _unitOfWork.Complete();

        //        if (!(result > 0))
        //            return BadRequest(
        //                new GeneralResponse<string>
        //                {
        //                    Success = false,
        //                    Message = $"Failed to delete book with id {id}"
        //                });

        //        return Ok(
        //            new GeneralResponse<string>
        //            {
        //                Success = true,
        //                Message = $"Deleted successfuly!"
        //            });
        //    }
        #endregion
    }
}
