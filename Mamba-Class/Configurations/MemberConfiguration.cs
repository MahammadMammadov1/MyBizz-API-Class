using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba_Class.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(50);
            
            builder.Property(x => x.InstaUrl)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(x => x.TwitUrl)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(x => x.FbUrl)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(x => x.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(100);
        }
    }
}
