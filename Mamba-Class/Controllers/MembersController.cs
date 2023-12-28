using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBizz.Business.Services.Interfaces;

namespace Mamba_Class.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMemberService _memberService;

        public MembersController(AppDbContext appDb, IMapper mapper, IWebHostEnvironment webHostEnvironment,IMemberService memberService)
        {
            this._appDb = appDb;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
            this._memberService = memberService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            await _memberService.GetAllAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            await _memberService.GetByIdAsync(id);
            return Ok();
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] MemberCreateDto dto)
        {
            await _memberService.CreateAsync(dto);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Update(int id, [FromForm]MemberUpdateDto dto)
        {
            await _memberService.UpdateAsync(id, dto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _memberService.Delete(id);    

            return NoContent();
        }
    }
}
