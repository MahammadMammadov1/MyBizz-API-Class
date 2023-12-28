using Mamba_Class.Configurations;
using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;

namespace Mamba_Class.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContext) : base(dbContext) 
        {
            
        }

        public DbSet<Member> Members { get; set; }  
        public DbSet<Profession> Professions { get; set; }  
        public DbSet<MemberProfession> MemberProfessions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemberConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
