using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mamba_Class.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;

        public ProfessionsController(AppDbContext appDb ,IMapper mapper )
        {
            this._appDb = appDb;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var prof = _appDb.Professions.ToList();
            IEnumerable<ProfessionGetDto> professionGets = prof.Select(p => new ProfessionGetDto 
            {
                Name = p.Name,
                CreatedDate = p.CreatedTime,
                UpdatedDate = p.CreatedTime,

            });

            return Ok(professionGets);
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var prof = _appDb.Professions.FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("bu idli data yoxdur");

            ProfessionGetDto professionGet = _mapper.Map<ProfessionGetDto>(prof);
            return Ok(professionGet);
        }
        [HttpPost]
        public IActionResult Create(ProfessionCreateDto professionCreate)
        {
            if (professionCreate == null) return BadRequest("bu idli data yoxdur");
            Profession profession = _mapper.Map<Profession>(professionCreate);
            _appDb.Professions.Add(profession);
            _appDb.SaveChanges();
            return Ok(profession);  
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _appDb.Professions.FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("bu idli data yoxdur");  
            _appDb.Professions.Remove(prof);
            _appDb.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProfessionUpdateDto update)
        {
            var prof = _appDb.Professions.FirstOrDefault(p => p.Id == id);
            if(prof == null) return BadRequest("bu idli data yoxdur");

            prof = _mapper.Map(update,prof);
            _appDb.SaveChanges();
            return Ok(prof);
        }
    }
}
