using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RopeyDVDSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<SystemUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SystemUserEntityConfiguration());
        }
    }

    public class SystemUserEntityConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255); 
            builder.Property(u => u.LastName).HasMaxLength(255); 
        }
    }
}
