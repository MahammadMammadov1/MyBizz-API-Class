using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba_Class.Configurations
{
    public class MemberProfessionConfiguration : IEntityTypeConfiguration<MemberProfession>
    {
        public void Configure(EntityTypeBuilder<MemberProfession> builder)
        {
            builder.HasOne(x => x.Member).WithMany(x => x.MemberProfessions).HasForeignKey(x => x.MemberId);
            builder.HasOne(x => x.Profession).WithMany(x => x.MemberProfessions).HasForeignKey(x => x.ProfessionId);
        }
    }
}
