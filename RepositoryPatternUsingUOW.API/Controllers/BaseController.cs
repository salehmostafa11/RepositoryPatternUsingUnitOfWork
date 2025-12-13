using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternUsingUOW.Core.DTOs;
using RepositoryPatternUsingUOW.Core.DTOs.Books;
using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace RepositoryPatternUsingUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TCreateDto, TGetDto, TUpdateDto> : ControllerBase where TEntity : class
    {

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        protected abstract IBaseRepository<TEntity> Repository { get; }

        [HttpGet("get-all")]
        public virtual async Task<ActionResult<GeneralResponse<IEnumerable<TGetDto>>>> GetAll()
        {
            var result = await Repository.GetAll();

            var mappedData = _mapper.Map<IEnumerable<TGetDto>>(result);

            return Ok(new GeneralResponse<IEnumerable<TGetDto>> { Success = true, Message = $"{mappedData.Count()} found!", Data = mappedData });
        }

        [HttpGet("get-by-id/{id:int}")]
        public virtual async Task<ActionResult<GeneralResponse<TGetDto>>> GetById(int id)
        {
            var entity = await Repository.GetByCriteriaIncluded(b => Microsoft.EntityFrameworkCore.EF.Property<int>(b,"Id") == id);
            if (entity == null)
            {
                return NotFound(
                    new GeneralResponse<TGetDto>
                    {
                        Success = false,
                        Message = $"Entity with id {id} not found!"
                    });
            }

            var mappedData = _mapper.Map<TGetDto>(entity);
            return Ok(
                new GeneralResponse<TGetDto>
                {
                    Success = true,
                    Message = "Entity found!",
                    Data = mappedData
                });
        }

         [HttpGet("search/{name:alpha}")]
        public virtual async Task<ActionResult<GeneralResponse<IEnumerable<TGetDto>>>> Search(string name)
        {
            var result = await Repository.SearchIncluded(e=>Microsoft.EntityFrameworkCore.EF.Property<string>(e,"Name") == name);
            if(result == null)
            {
                return BadRequest(
                    new GeneralResponse<IEnumerable<TGetDto>>
                    {
                        Success = false,
                        Message = "Connection error!"
                    });
            }

            var mappedData = _mapper.Map<IEnumerable<TGetDto>>(result);
            return Ok(new GeneralResponse<IEnumerable<TGetDto>>
            {
                Success= true,
                Message = $"{mappedData.Count()} found.",
                Data = mappedData
            });
        }


        [HttpPost("add")]
        public virtual async Task<ActionResult<GeneralResponse<TCreateDto>>> Add(TCreateDto entity)
        {
            var mappedData = _mapper.Map<TEntity>(entity);
            Repository.Add(mappedData);
            var result = await Task.FromResult(_unitOfWork.Complete());

            if (!(result > 0))
                return Ok(new GeneralResponse<TCreateDto>
                {
                    Success = false,
                    Message = "Failed to add!",
                    Data = entity
                });

            return Ok(new GeneralResponse<TCreateDto>
            {
                Success = true,
                Message = "Added Succesfully!",
                Data = entity
            });
        }
        
        [HttpDelete("delete")]
        public virtual async Task<ActionResult<GeneralResponse<string>>> Delete(int id)
        {
            var entity = await Repository.GetByCriteriaIncluded(e=>Microsoft.EntityFrameworkCore.EF.Property<int>(e,"Id") == id);

            if (entity == null) return NotFound(
                new GeneralResponse<string>
                {
                    Success = false,
                    Message = $"Entity with id {id} not found!"
                });

            Repository.Delete(entity);
            var result = _unitOfWork.Complete();

            if (!(result > 0))
                return BadRequest(
                    new GeneralResponse<string>
                    {
                        Success = false,
                        Message = $"Failed to delete item with id {id}"
                    });

            return Ok(
                new GeneralResponse<string>
                {
                    Success = true,
                    Message = $"Deleted successfuly!"
                });
        }
        [HttpPut("update")]
        public virtual async Task<ActionResult<GeneralResponse<TUpdateDto>>> Update(int id, TUpdateDto entity)
        {
            var objFromDb = await Repository.GetByCriteriaIncluded(e=>Microsoft.EntityFrameworkCore.EF.Property<int>(e,"Id") ==  id);
            if (objFromDb == null)
                return NotFound(new GeneralResponse<TUpdateDto>
                {
                    Success = false,
                    Message = "Entity not found!"
                });

            _mapper.Map(entity, objFromDb);

            Repository.Update(objFromDb);

            var result = _unitOfWork.Complete();
            if (!(result >= 0))
                return BadRequest(new GeneralResponse<TUpdateDto>
                {
                    Success = false,
                    Message = "Filed to update entity details!"
                });

            return Ok(new GeneralResponse<TUpdateDto>
            {
                Success = true,
                Message = "Updated successfuly!",
                Data = entity
            });
        }
    }
}
