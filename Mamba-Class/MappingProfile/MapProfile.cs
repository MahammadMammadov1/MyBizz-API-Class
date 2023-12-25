using AutoMapper;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.DTOs.ProfessionDto;
using Mamba_Class.Entites;

namespace Mamba_Class.MappingProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MemberGetDto,Member>().ReverseMap();
            CreateMap<MemberUpdateDto,Member>().ReverseMap();
            CreateMap<MemberCreateDto,Member>().ReverseMap();

            CreateMap<ProfessionCreateDto, Profession>().ReverseMap();
            CreateMap<ProfessionUpdateDto, Profession>().ReverseMap();
            CreateMap<ProfessionGetDto, Profession>().ReverseMap();

        }
    }
}
