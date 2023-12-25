using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mamba_Class.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MembersController(AppDbContext appDb, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this._appDb = appDb;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var prof = _appDb.Members.ToList();
            IEnumerable<MemberGetDto> memberGets = prof.Select(p => new MemberGetDto
            {
                FullName = p.FullName,
                InstaUrl = p.InstaUrl,
                FbUrl = p.FbUrl,
                TwitUrl = p.TwitUrl

            });

            return Ok(memberGets);
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var prof = _appDb.Members.FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("bu idli data yoxdur");

            MemberGetDto MemberGet = _mapper.Map<MemberGetDto>(prof);
            return Ok(MemberGet);
        }
        [HttpPost]
        public IActionResult Create([FromForm] MemberCreateDto dto)
        {
            string fileName = "";
            Member member = _mapper.Map<Member>(dto);
            if (dto.FormFile != null)
            {
                fileName = dto.FormFile.FileName;
                if (dto.FormFile.ContentType != "image/jpeg" && dto.FormFile.ContentType != "image/png")
                {
                    return BadRequest();
                }

                if (dto.FormFile.Length > 1048576)
                {
                    return BadRequest();

                }

                if (dto.FormFile.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\Mamba-Class\\Mamba-Class\\wwwroot\\Uploads\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    dto.FormFile.CopyTo(fileStream);
                }
                member.ImageUrl = fileName;
            }

            if (dto.ProfessionsIds != null)
            {
                foreach (var item in dto.ProfessionsIds)
                {
                    MemberProfession memberProfession = new MemberProfession
                    {
                        ProfessionId = item,
                        Member = member,

                    };
                    _appDb.MemberProfessions.Add(memberProfession);

                }
            }


            _appDb.Members.Add(member);
            _appDb.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm]MemberUpdateDto dto)
        {
            var member = _appDb.Members.FirstOrDefault(x => x.Id == id);
            if (member == null) return BadRequest();

            member = _mapper.Map(dto, member);


            string oldFilePath = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\Mamba-Class\\Mamba-Class\\wwwroot\\Uploads\\" + member.ImageUrl;

            if (dto.FormFile != null)
            {

                string newFileName = dto.FormFile.FileName;
                if (dto.FormFile.ContentType != "image/jpeg" && dto.FormFile.ContentType != "image/png")
                {
                    return BadRequest();
                }

                if (dto.FormFile.Length > 1048576)
                {
                    return BadRequest();

                }

                if (dto.FormFile.FileName.Length > 64)
                {
                    newFileName = newFileName.Substring(newFileName.Length - 64, 64);
                }

                newFileName = Guid.NewGuid().ToString() + newFileName;

                string newFilePath = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\Mamba-Class\\Mamba-Class\\wwwroot\\Uploads\\" + newFileName;
                using (FileStream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    dto.FormFile.CopyTo(fileStream);
                }



                member.ImageUrl = newFileName;

            }

            

            _appDb.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _appDb.Members.FirstOrDefault(x => x.Id == id);
            if (prof != null)
            {
                _appDb.Remove(prof);
                _appDb.SaveChanges();
            }

            return NoContent();
        }
    }
}
