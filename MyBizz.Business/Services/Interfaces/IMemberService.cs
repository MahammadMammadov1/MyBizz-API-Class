using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.DTOs.ProfessionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Business.Services.Interfaces
{
    public  interface IMemberService 
    {
        Task CreateAsync(MemberCreateDto dto);
        Task Delete(int id);
        Task<MemberGetDto> GetByIdAsync(int? id);
        Task<List<MemberGetDto>> GetAllAsync();
        Task UpdateAsync(int id, MemberUpdateDto dto);
    }
}
