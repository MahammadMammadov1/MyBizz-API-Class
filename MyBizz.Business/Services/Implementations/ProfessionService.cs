using AutoMapper;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using MyBizz.Business.Services.Interfaces;
using MyBizz.Core.Repositories.Interfaces;
using MyBizz.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyBizz.Business.Services.Implementations
{
    public class ProfessionService : IProfessionService
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IProfessionRepository _professionRepository;

        public ProfessionService(AppDbContext appDb, IMapper mapper,IProfessionRepository professionRepository)
        {
            this._appDb = appDb;
            this._mapper = mapper;
            this._professionRepository = professionRepository;
        }
        public async Task CreateAsync(ProfessionCreateDto dto)
        {
            if(dto == null) throw new ArgumentNullException(nameof(dto));
            Profession profession = _mapper.Map<Profession>(dto);
            await _professionRepository.CreateAsync(profession);
            await _professionRepository.CommitAsync();
            

        }

        public async Task Delete(int id)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == id);
            if (prof == null) throw new NullReferenceException(nameof(prof));
         
            _professionRepository.DeleteAsync(prof);
            await _professionRepository.CommitAsync();
        }

        public async Task<List<ProfessionGetDto>> GetAllAsync()
        {
            var profession = await _professionRepository.GetAllAsync();
            return _mapper.Map<List<ProfessionGetDto>>(profession);
        }

        public async Task<ProfessionGetDto> GetByIdAsync(int? id)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == id);
            if (prof == null) throw new Exception();
            ProfessionGetDto professionGet = _mapper.Map<ProfessionGetDto>(prof);
            return professionGet;
        }

        public async Task UpdateAsync(int id , ProfessionUpdateDto dto)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == id);
            if (prof == null) throw new NullReferenceException();

            prof = _mapper.Map(dto, prof);
            await _professionRepository.CommitAsync();
        }
    }
}
