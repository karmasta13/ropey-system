using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
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

            // Relationship mapping for DVDCopy
            modelBuilder.Entity<DVDCopy>().HasOne(dt => dt.DVDTitle).WithMany(dt => dt.DVDCopies).HasForeignKey(dt => dt.DVDNumber);

            // Relationship mapping for DVDTitle
            modelBuilder.Entity<DVDTitle>().HasOne(dt => dt.DVDCategory).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.CategoryNumber);
            modelBuilder.Entity<DVDTitle>().HasOne(dt => dt.Producer).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.ProducerNumber);
            modelBuilder.Entity<DVDTitle>().HasOne(dt => dt.Studio).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.StudioNumber);
            // Property restrictions for DVDTitle
            modelBuilder.Entity<DVDTitle>().Property(dt => dt.PenaltyCharge).HasPrecision(10, 2);
            modelBuilder.Entity<DVDTitle>().Property(dt => dt.StandardCharge).HasPrecision(10, 2);

            // Relationship mapping for Loan
            modelBuilder.Entity<Loan>().HasOne(dt => dt.DVDCopy).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.CopyNumber);
            modelBuilder.Entity<Loan>().HasOne(dt => dt.LoanType).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.LoanTypeNumber);
            modelBuilder.Entity<Loan>().HasOne(dt => dt.Member).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.MemberNumber);

            // Relationship mapping for Members
            modelBuilder.Entity<Member>().HasOne(dt => dt.MembershipCategory).WithMany(dt => dt.Members).HasForeignKey(dt => dt.MemberCategoryNumber);

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
