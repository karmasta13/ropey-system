using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CastMember>().HasKey(cm => new
            {
                cm.ActorNumber,
                cm.DVDNumber
            });

            modelBuilder.Entity<CastMember>().HasOne(a => a.Actor).WithMany(cm => cm.CastMembers).HasForeignKey(a => a.ActorNumber);
            modelBuilder.Entity<CastMember>().HasOne(a => a.DVDTitle).WithMany(cm => cm.CastMembers).HasForeignKey(a => a.DVDNumber);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<DVDCategory> DVDCategories { get; set; }
        public DbSet<DVDCopy> DVDCopies { get; set; }
        public DbSet<DVDTitle> DVDTitles { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipCategory> MembershipCategories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Studio> Studios { get; set; }
    }
}
