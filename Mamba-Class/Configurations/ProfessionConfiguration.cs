using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mamba_Class.Configurations
{
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);
        }
    }
}
