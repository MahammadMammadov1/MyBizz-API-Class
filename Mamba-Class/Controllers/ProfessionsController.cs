using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBizz.Business.Services.Interfaces;

namespace Mamba_Class.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IProfessionService _professionService;

        public ProfessionsController(AppDbContext appDb ,IMapper mapper ,IProfessionService professionService)
        {
            this._appDb = appDb;
            this._mapper = mapper;
            this._professionService = professionService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAll()
        {
            List<ProfessionGetDto> prof = await _professionService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, prof);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]

        public async Task<IActionResult> Get(int id)
        {
            var prof =await _professionService.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, prof);


        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]

        public async Task<IActionResult> Create(ProfessionCreateDto professionCreate)
        {
            await _professionService.CreateAsync(professionCreate);
            return Ok();  
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _professionService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ProfessionUpdateDto update)
        {
            await _professionService.UpdateAsync(id, update);
            return NoContent();
        }
    }
}
