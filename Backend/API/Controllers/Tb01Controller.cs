using Application.DTOs;
using Application.Interfaces;
using Application.Response;
using Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tb01Controller : ControllerBase
    {
        private readonly ITb01Service _service;

        public Tb01Controller(ITb01Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<Tb01DTO>>> GetAll([FromQuery] PaginationParametersDTO paginationParameters)
        {
            var tb01 = await _service.GetAll(paginationParameters);

            if (tb01 != null)
            {
                return Ok(tb01);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            var response = await _service.Count();
            return Ok(response);
        }


        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Tb01DTO>> GetById(int id)
        {
            
            var response = await _service.GetById(id);
            if (response != null)
            {
                return Ok(response);
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<MessageResponse>> Create([FromBody] Tb01DTO tb01DTO)
        {
            try
            {
                await _service.Create(tb01DTO);
                return new CreatedAtRouteResult("GetById", new { id = tb01DTO.Id }, tb01DTO);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<MessageResponse>> Update([FromBody] Tb01DTO tb01DTO)
        {
            try
            {
                await _service.Update(tb01DTO);
                return Ok(tb01DTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<MessageResponse>> Delete([FromBody] Tb01DTO tb01DTO)
        {
            try
            {
                var response = await _service.Delete(tb01DTO);
                if (response.Success)
                {
                    return NoContent();
                } 
                else
                {
                    return NotFound();
                }
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
