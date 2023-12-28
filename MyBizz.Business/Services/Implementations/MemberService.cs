using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBizz.Business.Services.Interfaces;
using MyBizz.Core.Repositories.Interfaces;
using MyBizz.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Business.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public MemberService(AppDbContext appDb, IMapper mapper, IMemberRepository memberRepository)
        {
            
            _appDb = appDb;
            
            _mapper = mapper;
            
            _memberRepository = memberRepository;
        }
        public async Task CreateAsync([FromForm]MemberCreateDto dto)
        {
            string fileName = "";
            Member member = _mapper.Map<Member>(dto);
            if (dto.FormFile != null)
            {
                fileName = dto.FormFile.FileName;
                if (dto.FormFile.ContentType != "image/jpeg" && dto.FormFile.ContentType != "image/png")
                {
                    throw new Exception();
                }

                if (dto.FormFile.Length > 1048576)
                {
                    throw new Exception();

                }

                if (dto.FormFile.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MyBizz-API-Class\\Mamba-Class\\wwwroot\\Uploads\\" + fileName;
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
                    if (!_appDb.Professions.All(x => x.Id == item)) throw new Exception();
                    MemberProfession memberProfession = new MemberProfession
                    {
                        ProfessionId = item,
                        Member = member,

                    };
                    _appDb.MemberProfessions.Add(memberProfession);

                }
            }


            //_appDb.Members.Add(member);
            await _memberRepository.CreateAsync(member);
            await _memberRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var prof =await _memberRepository.GetByIdAsync(x=>x.Id==id);
            if (prof == null) throw new NullReferenceException(nameof(prof));

            _memberRepository.DeleteAsync(prof);
            await _memberRepository.CommitAsync();
        }

        public async Task<List<MemberGetDto>> GetAllAsync()
        {
            var member = await _memberRepository.GetAllAsync();
            return _mapper.Map<List<MemberGetDto>>(member);
        }

        public async Task<MemberGetDto> GetByIdAsync(int? id)
        {
            var member = await _memberRepository.GetByIdAsync(x => x.Id == id);
            if (member == null) throw new Exception();
            MemberGetDto professionGet = _mapper.Map<MemberGetDto>(member);
            return professionGet;
        }

        public async Task UpdateAsync(int id, MemberUpdateDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(x => x.Id == id);
            if (member == null) throw new Exception();

            member = _mapper.Map(dto, member);


            string oldFilePath = "C:\\Users\\Mehemmed\\Desktop\\MyBizz-API-Class\\Mamba-Class\\wwwroot\\Uploads\\" + member.ImageUrl;

            if (dto.FormFile != null)
            {

                string newFileName = dto.FormFile.FileName;
                if (dto.FormFile.ContentType != "image/jpeg" && dto.FormFile.ContentType != "image/png")
                {
                    throw new Exception();
                }

                if (dto.FormFile.Length > 3000000)
                {
                    throw new Exception();

                }

                if (dto.FormFile.FileName.Length > 64)
                {
                    newFileName = newFileName.Substring(newFileName.Length - 64, 64);
                }

                newFileName = Guid.NewGuid().ToString() + newFileName;

                string newFilePath = "C:\\Users\\Mehemmed\\Desktop\\MyBizz-API-Class\\Mamba-Class\\wwwroot\\Uploads\\" + newFileName;
                using (FileStream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    dto.FormFile.CopyTo(fileStream);
                }
                member.ImageUrl = newFileName;

            }

            if (dto.ProfessionsIds != null)
            {
                member.MemberProfessions.RemoveAll(bt => dto.ProfessionsIds == null || !dto.ProfessionsIds.Contains(bt.ProfessionId));
                foreach (var profId in dto.ProfessionsIds.Where(tagId => !member.MemberProfessions.Any(bt => bt.ProfessionId == tagId)))
                {
                    MemberProfession bookTag = new MemberProfession
                    {
                        ProfessionId = profId
                    };
                    member.MemberProfessions.Add(bookTag);
                }
            }

            await _memberRepository.CommitAsync();
        }
    }
}
