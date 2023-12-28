namespace Mamba_Class.Entites
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; }    
        public string InstaUrl { get; set; }
        public string FbUrl { get; set; }
        public string TwitUrl { get; set; }
        public string ImageUrl { get; set; } 
        
        public List<MemberProfession> MemberProfessions { get; set; }

    }
}
