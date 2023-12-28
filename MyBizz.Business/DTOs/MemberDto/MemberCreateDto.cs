using Microsoft.AspNetCore.Http;

namespace Mamba_Class.DTOs.MemberDto
{
    public class MemberCreateDto
    {
        public string FullName { get; set; }
        public string InstaUrl { get; set; }
        public string TwitUrl { get; set; }
        public string FbUrl { get; set; }
        public IFormFile FormFile { get; set; }

        public List<int> ProfessionsIds { get; set; }
    }
}
