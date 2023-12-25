namespace Mamba_Class.Entites
{
    public class MemberProfession : BaseEntity
    {
        public int ProfessionId { get; set; }
        public int MemberId { get; set; }
        public Profession Profession { get; set; }
        public Member Member { get; set; }
    }
}
