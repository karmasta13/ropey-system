using Microsoft.AspNetCore.Identity;
using RopeyDVDSystem.Models;
using RopeyDVDSystem.Models.Identity;

namespace RopeyDVDSystem.Data
{
    public class ApplicationDBInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

                if (!await roleManager.RoleExistsAsync(UserRoles.Assistant))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Assistant));

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@ropey.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Ropey Admin",
                        UserName = "AdminUser",
                        Email = adminUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Manager);
                }


                string appUserEmail = "user@ropey.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application Assistant",
                        UserName = "app-user",
                        Email = appUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Assistant);
                }

            }

        }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // Actors 
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {

                            ActorPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ActorFirstName = "Robert",
                            ActorSurname = "Pattinson"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                          ActorFirstName = "Robert2",
                          ActorSurname = "Pattinson2"
                        },
                        new Actor()
                        {
                            ActorPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ActorFirstName = "Robert3",
                            ActorSurname = "Pattinson3"
                        },
                        new Actor()
                        {
                            ActorPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ActorFirstName = "Robert4",
                            ActorSurname = "Pattinson4"
                        },
                    });
                    context.SaveChanges();


                }

                if (!context.DVDCategories.Any())
                {
                    context.DVDCategories.AddRange(new List<DVDCategory>()
                    {
                        new DVDCategory()
                        {
                            CategoryName = "Category 1",
                            CategoryDescription = "This is category 1",
                            AgeRestricted = "+18"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Category 2",
                            CategoryDescription = "This is category 2",
                            AgeRestricted = "+18"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Category 3",
                            CategoryDescription = "This is category 3",
                            AgeRestricted = "+18"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Category 4",
                            CategoryDescription = "This is category 4",
                            AgeRestricted = "+18"
                        },
                    });
                    context.SaveChanges();
                }

                //Studio
                if (!context.Studios.Any())
                {
                    context.Studios.AddRange(new List<Studio>()
                    {
                        new Studio()
                        {
                            StudioName = "Studio Name 1"
                        },
                        new Studio()
                        {
                            StudioName = "Studio Name 1"
                        },
                        new Studio()
                        {
                            StudioName = "Studio Name 1"
                        },
                        new Studio()
                        {
                            StudioName = "Studio Name 1"
                        },
                    });
                    context.SaveChanges();
                }

                // Producer
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            ProducerPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ProducerName = "Producer number 1",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ProducerName = "Producer number 2",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ProducerName = "Producer number 3",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ProducerName = "Producer number 4",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            ProducerName = "Producer number 5",
                        },
                    });
                    context.SaveChanges();
                }

                //DVDTitle
                if (!context.DVDTitles.Any())
                {
                    context.DVDTitles.AddRange(new List<DVDTitle>()
                    {
                        new DVDTitle()
                        {
                            CategoryNumber = 1,
                            StudioNumber = 1,
                            ProducerNumber = 1,
                            DVDPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            DVDTitleName = "Title 1",
                            DateReleased = DateTime.Now.AddDays(-10),
                            StandardCharge = 15.98m,
                            PenaltyCharge = 15.76m
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 4,
                            StudioNumber = 2,
                            ProducerNumber = 2,
                            DVDPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            DVDTitleName = "Title 2",
                            DateReleased = DateTime.Now.AddDays(-10),
                            StandardCharge = 10.4m,
                            PenaltyCharge = 15.76m
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 2,
                            StudioNumber = 1,
                            ProducerNumber = 4,
                            DVDPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            DVDTitleName = "Title 3",
                            DateReleased = DateTime.Now.AddDays(-10),
                            StandardCharge = 15.98m,
                            PenaltyCharge = 15.76m
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 3,
                            StudioNumber = 4,
                            ProducerNumber = 3,
                            DVDPictureURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            DVDTitleName = "Title 4",
                            DateReleased = DateTime.Now.AddDays(-10),
                            StandardCharge = 15.98m,
                            PenaltyCharge = 15.76m
                        },
                    });
                    context.SaveChanges();
                }

                //CastMember
                if (!context.CastMembers.Any())
                {
                    context.CastMembers.AddRange(new List<CastMember>()
                    {
                        new CastMember()
                        {
                            ActorNumber= 1,
                            DVDNumber = 3
                        },
                        new CastMember()
                        {
                            ActorNumber= 2,
                            DVDNumber = 4
                        },
                        new CastMember()
                        {
                            ActorNumber= 1,
                            DVDNumber = 5
                        },

                    });
                    context.SaveChanges();
                }

                //DVDCopy
                if (!context.DVDCopies.Any())
                {
                    context.DVDCopies.AddRange(new List<DVDCopy>()
                    {
                        new DVDCopy()
                        {
                            DVDNumber = 5,
                            DatePurchased = DateTime.Now.AddDays(-20)
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 4,
                            DatePurchased = DateTime.Now.AddDays(-20)
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 3,
                            DatePurchased = DateTime.Now.AddDays(-20)
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 2,
                            DatePurchased = DateTime.Now.AddDays(-20)
                        },
                    });
                    context.SaveChanges();
                }

                //LoanType
                if (!context.LoanTypes.Any())
                {
                    context.LoanTypes.AddRange(new List<LoanType>()
                    {
                        new LoanType()
                        {
                            LoanTypeName = "Loan Type 1",
                            Duration = 10
                        },
                        new LoanType()
                        {
                            LoanTypeName = "Loan Type 2",
                            Duration = 10
                        },
                        new LoanType()
                        {
                            LoanTypeName = "Loan Type 3",
                            Duration = 10
                        },
                        new LoanType()
                        {
                            LoanTypeName = "Loan Type 4",
                            Duration = 10
                        },
                    });
                    context.SaveChanges();
                }

                // MembershipCategory
                if (!context.MembershipCategories.Any())
                {
                    context.MembershipCategories.AddRange(new List<MembershipCategory>()
                    {
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Membership 1",
                            MembershipCategoryDescription = "This is type 1 of membershup category",
                            MembershipCategoryTotalLoans = 1,
                        },
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Membership 2",
                            MembershipCategoryDescription = "This is type 2 of membershup category",
                            MembershipCategoryTotalLoans = 2,
                        },
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Membership 3",
                            MembershipCategoryDescription = "This is type 3 of membershup category",
                            MembershipCategoryTotalLoans = 3,
                        },
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Membership 4",
                            MembershipCategoryDescription = "This is type 4 of membershup category",
                            MembershipCategoryTotalLoans = 4,
                        },
                    });
                    context.SaveChanges();
                }

                // Member
                if (!context.Members.Any())
                {
                    context.Members.AddRange(new List<Member>()
                    {
                        new Member()
                        {
                            MemberCategoryNumber = 1,
                            MemberFirstName = "Robert",
                            MemberLastName = "Pattinson",
                            MemberAddress = "Kuleshwor",
                            MemberDateOfBirth = DateTime.Now.AddYears(-10),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 1,
                            MemberFirstName = "Robert",
                            MemberLastName = "Pattinson",
                            MemberAddress = "Kuleshwor",
                            MemberDateOfBirth = DateTime.Now.AddYears(-10),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Robert",
                            MemberLastName = "Pattinson",
                            MemberAddress = "Kuleshwor",
                            MemberDateOfBirth = DateTime.Now.AddYears(-10),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 2,
                            MemberFirstName = "Robert",
                            MemberLastName = "Pattinson",
                            MemberAddress = "Kuleshwor",
                            MemberDateOfBirth = DateTime.Now.AddYears(-10),
                        },
                    });
                    context.SaveChanges();
                }

                //Loan
                if (!context.Loans.Any())
                {
                    context.Loans.AddRange(new List<Loan>()
                    {
                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 5,
                            MemberNumber = 1,
                            DateOut = DateTime.Now.AddDays(-10),
                            DateDue = DateTime.Now.AddDays(10),
                            DateReturn = DateTime.Now.AddDays(-5)
                        },
                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 8,
                            MemberNumber = 3,
                            DateOut = DateTime.Now.AddDays(-10),
                            DateDue = DateTime.Now.AddDays(10),
                            DateReturn = DateTime.Now.AddDays(-5)
                        },
                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 7,
                            MemberNumber = 3,
                            DateOut = DateTime.Now.AddDays(-10),
                            DateDue = DateTime.Now.AddDays(10),
                            DateReturn = DateTime.Now.AddDays(-5)
                        },
                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 6,
                            MemberNumber = 4,
                            DateOut = DateTime.Now.AddDays(-10),
                            DateDue = DateTime.Now.AddDays(10),
                            DateReturn = DateTime.Now.AddDays(-5)
                        },
                    });
                    context.SaveChanges();
                }

            }
        }

    }
}
