using Mamba_Class.DTOs.ProfessionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Business.Services.Interfaces
{
    public interface IProfessionService 
    {
        Task CreateAsync(ProfessionCreateDto dto);
        Task Delete(int id);
        Task<ProfessionGetDto> GetByIdAsync(int? id);
        Task<List<ProfessionGetDto>> GetAllAsync();
        Task UpdateAsync(int id , ProfessionUpdateDto dto);
    }
}
