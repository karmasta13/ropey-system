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
                        FullName = "Ropey Assistant",
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
                          ActorPictureURL = "https://i.postimg.cc/BvB3b7pX/lindsay.jpg",
                          ActorFirstName = "Lindsay",
                          ActorSurname = "Lohan"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/C5Z9GcGC/rachel-mcadams-435-40.jpg",
                          ActorFirstName = "Rachel",
                          ActorSurname = "McAdams"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/0jW2QX8p/Amanda-Seyfried-2019-by-Glenn-Francis.jpg",
                          ActorFirstName = "Amanda",
                          ActorSurname = "Seyfried"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/k5s5zgk1/timothee.jpg",
                          ActorFirstName = "Timothee",
                          ActorSurname = "Chalamet"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/hPCWk7j7/zendaya.jpg",
                          ActorFirstName = "Zendaya",
                          ActorSurname = "Coleman"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/8kLtfFrf/jason.jpg",
                          ActorFirstName = "Jason",
                          ActorSurname = "Momoa"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/W3Tt9fXv/Sam-Claflin.jpg",
                          ActorFirstName = "Sam",
                          ActorSurname = "Claflin"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/4y106nnG/Emilia.jpg",
                          ActorFirstName = "Emilia",
                          ActorSurname = "Clarke"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/Kz2Q0F68/Matthew-Lewis.jpg",
                          ActorFirstName = "Matthew",
                          ActorSurname = "Lewis"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/rw2d0mxs/Robert-Pattinson.jpg",
                          ActorFirstName = "Robert",
                          ActorSurname = "Pattinson"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/yxwTSHyK/Kravitz.jpg",
                          ActorFirstName = "Zoe",
                          ActorSurname = "Kravitz"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/6pDyJQ5t/Paul-Dano.jpg",
                          ActorFirstName = "Paul",
                          ActorSurname = "Dano"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/C51FfyLc/Colin-Farrell.jpg",
                          ActorFirstName = "Colin",
                          ActorSurname = "Farrell"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/YCkt1m94/James-Franco.jpg",
                          ActorFirstName = "James",
                          ActorSurname = "Franco"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/xT544k5N/Selena-Gomez.png",
                          ActorFirstName = "Selena",
                          ActorSurname = "Gomez"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/SRDHCjt9/Vanessa-Hudgens.jpg",
                          ActorFirstName = "Vanessa",
                          ActorSurname = "Hudgens"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/vZWLNCbq/Kate-Winslet.jpg",
                          ActorFirstName = "Kate",
                          ActorSurname = "Winslet"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/ZYBsJbm0/Leonardo-Di-Caprio.jpg",
                          ActorFirstName = "Leonardo",
                          ActorSurname = "DiCaprio"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/pTtryV7x/Billy-Zane.jpg",
                          ActorFirstName = "Billy",
                          ActorSurname = "Zane"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/7Ljs2dMq/Tom-Hardy.jpg",
                          ActorFirstName = "Tom",
                          ActorSurname = "Hardy"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/JhjJpFCr/Will-Poulter.jpg",
                          ActorFirstName = "Will",
                          ActorSurname = "Poulter"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/MKhgRcWC/Zoe-Saldana.jpg",
                          ActorFirstName = "Zoe",
                          ActorSurname = "Saldana"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/wj5yYm3H/Sam-Worthington.jpg",
                          ActorFirstName = "Sam",
                          ActorSurname = "Worthington"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/d0LhkS4Q/Sigourney-Weaver.jpg",
                          ActorFirstName = "Sigourney",
                          ActorSurname = "Weaver"
                        },
                        new Actor()
                        {
                          ActorPictureURL = "https://i.postimg.cc/zBkYJqKy/Michelle-Rodriguez.jpg",
                          ActorFirstName = "Michelle",
                          ActorSurname = "Rodriguez"
                        },
                    });
                    context.SaveChanges();


                }

                //Category
                if (!context.DVDCategories.Any())
                {
                    context.DVDCategories.AddRange(new List<DVDCategory>()
                    {
                        new DVDCategory()
                        {
                            CategoryName = "Comedy",
                            CategoryDescription = "The comedy genre uses humor and familiarity, in both situations and characters, to appeal to audiences.",
                            AgeRestricted = "False"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Family",
                            CategoryDescription = "Family film is a genre that is contains appropriate content for younger viewers. Family film aims to appeal not only to children, but to a wide range of ages.While the storyline may appeal to a younger audience, there are components of the film that are geared towards adults - such as witty jokes and humor.",
                            AgeRestricted = "False"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Action",
                            CategoryDescription = "Action film is a film genre in which the protagonist is thrust into a series of events that typically involve violence and physical feats.",
                            AgeRestricted = "False"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Sci-fi",
                            CategoryDescription = "Science fiction (sometimes shortened to sci-fi or SF) is a genre of speculative fiction which typically deals with imaginative and futuristic concepts such as advanced science and technology, space exploration, time travel, parallel universes, and extraterrestrial life. ",
                            AgeRestricted = "True"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Romance",
                            CategoryDescription = "Romance films or movies involve romantic love stories recorded in visual media for broadcast in theatres or on television that focus on passion, emotion, and the affectionate romantic involvement of the main characters. Typically their journey through dating, courtship or marriage is featured.",
                            AgeRestricted = "False"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Adventure",
                            CategoryDescription = "Adventure film is a genre that revolves around the conquests and explorations of a protagonist. The purpose of the conquest can be to retrieve a person or treasure, but often the main focus is simply the pursuit of the unknown. ",
                            AgeRestricted = "False"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "Crime",
                            CategoryDescription = "Crime film is a genre that revolves around the action of a criminal mastermind. A Crime film will often revolve around the criminal himself, chronicling his rise and fall. Some Crime films will have a storyline that follows the criminal's victim, yet others follow the person in pursuit of the criminal.",
                            AgeRestricted = "True"
                        },
                        new DVDCategory()
                        {
                            CategoryName = "18+",
                            CategoryDescription = "Films classified R 18+ is restricted to adults. Some films classified R18+ may be offensive to sections of the adult community. A person may be asked for proof of their age before purchasing, hiring or viewing R18 + films at a retail store or cinema.",
                            AgeRestricted = "True"
                        },
                    });
                    context.SaveChanges();
                }

                if (!context.Studios.Any())
                {
                    context.Studios.AddRange(new List<Studio>()
                    {
                        new Studio()
                        {
                            StudioName = "Broadway Video"
                        },
                        new Studio()
                        {
                            StudioName = "Legendary Pictures"
                        },
                        new Studio()
                        {
                            StudioName = "Pinewood Studios"
                        },
                        new Studio()
                        {
                            StudioName = "Warner Bros Pictures"
                        },
                        new Studio()
                        {
                            StudioName = "Lightstorm Entertainment"
                        },
                        new Studio()
                        {
                            StudioName = "Paramount Pictures"
                        },
                        new Studio()
                        {
                            StudioName = "Regency Enterprises RatPac Entertainment"
                        },
                        new Studio()
                        {
                            StudioName = "Muse Entertainment"
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
                            ProducerPictureURL = "https://i.postimg.cc/Bb3rzHDz/Lorne-Michaels.jpg",
                            ProducerName = "Lorne Michaels",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "https://i.postimg.cc/tg02DyFS/Mary-Parent.jpg",
                            ProducerName = "Mary Parent",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "https://i.postimg.cc/W3VVBBcS/Alison-Owen.jpg",
                            ProducerName = "Alison Owen",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "https://i.postimg.cc/597cgYys/Matt-Reeves.jpg",
                            ProducerName = "Matt Reeves",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "https://i.postimg.cc/rFV1Vs92/Chris-Hanley.jpg",
                            ProducerName = "Chris Hanley",
                        },
                        new Producer()
                        {
                            ProducerPictureURL = "https://i.postimg.cc/CxZf5qBX/James-Cameron.jpg",
                            ProducerName = "James Cameron",
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
                            DVDPictureURL = "https://i.postimg.cc/0Ndz6RxK/mean-girls.jpg",
                            DVDTitleName = "Mean Girls",
                            DateReleased = DateTime.ParseExact("2004-04-30","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 15
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 4,
                            StudioNumber = 2,
                            ProducerNumber = 2,
                            DVDPictureURL = "https://i.postimg.cc/jS1pzLxG/dune.jpg",
                            DVDTitleName = "Dune",
                            DateReleased = DateTime.ParseExact("2021-10-22","yyyy-MM-dd",null),
                            StandardCharge = 500,
                            PenaltyCharge = 150
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 5,
                            StudioNumber = 3,
                            ProducerNumber = 3,
                            DVDPictureURL = "https://i.postimg.cc/dt1N4wxv/me-before-you.jpg",
                            DVDTitleName = "Me Before You",
                            DateReleased = DateTime.ParseExact("2016-08-30","yyyy-MM-dd",null),
                            StandardCharge = 400,
                            PenaltyCharge = 100
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 3,
                            StudioNumber = 4,
                            ProducerNumber = 4,
                            DVDPictureURL = "https://i.postimg.cc/zfJgFXxF/the-batman.jpg",
                            DVDTitleName = "The Batman",
                            DateReleased = DateTime.ParseExact("2022-03-04","yyyy-MM-dd",null),
                            StandardCharge = 600,
                            PenaltyCharge = 150,
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 7,
                            StudioNumber = 8,
                            ProducerNumber = 4,
                            DVDPictureURL = "https://i.postimg.cc/CxM8jxPH/spring-breakers.jpg",
                            DVDTitleName = "Spring Breakers",
                            DateReleased = DateTime.ParseExact("1997-12-19","yyyy-MM-dd",null),
                            StandardCharge = 300,
                            PenaltyCharge = 100
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 5,
                            StudioNumber = 6,
                            ProducerNumber = 6,
                            DVDPictureURL = "https://i.postimg.cc/4yZysWjb/titanic.jpg",
                            DVDTitleName = "Titanic",
                            DateReleased = DateTime.ParseExact("1997-12-17","yyyy-MM-dd",null),
                            StandardCharge = 200,
                            PenaltyCharge = 50,
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 7,
                            StudioNumber = 7,
                            ProducerNumber = 2,
                            DVDPictureURL = "https://i.postimg.cc/SRwZnz7r/the-renevant.jpg",
                            DVDTitleName = "The Renevant",
                            DateReleased = DateTime.ParseExact("2015-12-16","yyyy-MM-dd",null),
                            StandardCharge = 400,
                            PenaltyCharge = 100
                        },
                        new DVDTitle()
                        {
                            CategoryNumber = 4,
                            StudioNumber = 5,
                            ProducerNumber = 1,
                            DVDPictureURL = "https://i.postimg.cc/LsdV2gMV/A-New-Hope-poster.webp",
                            DVDTitleName = "Avatar",
                            DateReleased = DateTime.ParseExact("2009-12-17","yyyy-MM-dd",null),
                            StandardCharge = 250,
                            PenaltyCharge = 50
                        },
                         new DVDTitle()
                        {
                            DVDTitleName = "Avengers: Endgame",
                            CategoryNumber = 3,
                            StudioNumber = 2,
                            ProducerNumber = 3,
                            DateReleased = DateTime.ParseExact("2019-04-26","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/bN7ntXFy/MV5-BMTc5-MDE2-ODcw-NV5-BMl5-Ban-Bn-Xk-Ft-ZTgw-Mz-I2-Nz-Q2-Nz-M-V1.jpg",
                            StandardCharge = 300,
                            PenaltyCharge = 25
                        },
                         new DVDTitle()
                        {
                            DVDTitleName = "Star Wars: Episode IX",
                            CategoryNumber = 4,
                            StudioNumber = 7,
                            ProducerNumber = 4,
                            DateReleased = DateTime.ParseExact("2019-12-20","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/LsdV2gMV/A-New-Hope-poster.webp",
                            StandardCharge = 250,
                            PenaltyCharge = 25
                        },

                          new DVDTitle()
                        {
                            DVDTitleName = "Doctor Strange in the Multiverse of Madness",
                            CategoryNumber = 3,
                            StudioNumber = 1,
                            ProducerNumber = 3,
                            DateReleased = DateTime.ParseExact("2022-05-06","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/vHvxXWXS/Doctor-Strange-in-the-Multiverse-of-Madness-poster.jpg",
                            StandardCharge = 250,
                            PenaltyCharge = 30,
                        },

                          new DVDTitle()
                        {
                            DVDTitleName = "14 Peaks",
                            CategoryNumber = 4,
                            StudioNumber = 3,
                            ProducerNumber = 1,
                            DateReleased = DateTime.ParseExact("2021-11-29","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/T3WHGXBn/14-Peaks-Nothing-Is-Impossible.jpg",
                            StandardCharge = 400,
                            PenaltyCharge = 75,
                        },
                           new DVDTitle()
                        {
                            DVDTitleName = "Fight Club",
                            CategoryNumber = 3,
                            StudioNumber = 7,
                            ProducerNumber = 5,
                            DateReleased = DateTime.ParseExact("1999-09-11","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/L6Gy88dN/index.jpg",
                            StandardCharge = 250,
                            PenaltyCharge = 50,
                        },

                           new DVDTitle()
                        {
                            DVDTitleName = "John Wick",
                            CategoryNumber = 7,
                            StudioNumber = 5,
                            ProducerNumber = 4,
                            DateReleased = DateTime.ParseExact("2014-10-24","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/52wXQnrD/MV5-BMTU2-Nj-A1-ODgz-MF5-BMl5-Ban-Bn-Xk-Ft-ZTgw-MTM2-MTI4-Mj-E-V1.jpg",
                            StandardCharge = 150,
                            PenaltyCharge = 15
                        },
                             new DVDTitle()
                        {
                            DVDTitleName = "Black Widow",
                            CategoryNumber = 3,
                            StudioNumber = 6,
                            ProducerNumber = 2,
                            DateReleased = DateTime.ParseExact("2021-07-09","yyyy-MM-dd",null),
                            DVDPictureURL = "https://i.postimg.cc/R0LGd1WD/Black-Widow-2021-film-poster.jpg",
                            StandardCharge = 200,
                            PenaltyCharge = 20
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
                            DVDNumber = 1
                        },
                        new CastMember()
                        {
                            ActorNumber= 2,
                            DVDNumber = 1
                        },
                        new CastMember()
                        {
                            ActorNumber= 3,
                            DVDNumber = 1
                        },
                        new CastMember()
                        {
                            ActorNumber= 4,
                            DVDNumber = 2
                        },
                        new CastMember()
                        {
                            ActorNumber= 5,
                            DVDNumber = 2
                        },
                        new CastMember()
                        {
                            ActorNumber= 6,
                            DVDNumber = 2
                        },
                        new CastMember()
                        {
                            ActorNumber= 7,
                            DVDNumber = 3
                        },
                        new CastMember()
                        {
                            ActorNumber= 8,
                            DVDNumber = 3
                        },
                        new CastMember()
                        {
                            ActorNumber= 9,
                            DVDNumber = 3
                        },
                        new CastMember()
                        {
                            ActorNumber= 10,
                            DVDNumber = 4
                        },
                        new CastMember()
                        {
                            ActorNumber= 11,
                            DVDNumber = 4
                        },
                        new CastMember()
                        {
                            ActorNumber= 12,
                            DVDNumber = 4
                        },
                        new CastMember()
                        {
                            ActorNumber= 14,
                            DVDNumber = 5
                        },
                        new CastMember()
                        {
                            ActorNumber= 15,
                            DVDNumber = 5
                        },
                        new CastMember()
                        {
                            ActorNumber= 16,
                            DVDNumber = 5
                        },
                        new CastMember()
                        {
                            ActorNumber= 17,
                            DVDNumber = 6
                        },
                        new CastMember()
                        {
                            ActorNumber= 18,
                            DVDNumber = 6
                        },
                        new CastMember()
                        {
                            ActorNumber= 19,
                            DVDNumber = 6
                        },
                        new CastMember()
                        {
                            ActorNumber= 18,
                            DVDNumber = 7
                        },
                        new CastMember()
                        {
                            ActorNumber= 20,
                            DVDNumber = 7
                        },
                        new CastMember()
                        {
                            ActorNumber= 21,
                            DVDNumber = 7
                        },
                        new CastMember()
                        {
                            ActorNumber= 22,
                            DVDNumber = 8
                        },
                        new CastMember()
                        {
                            ActorNumber= 23,
                            DVDNumber = 9
                        },
                        new CastMember()
                        {
                            ActorNumber= 10,
                            DVDNumber = 10
                        },
                        new CastMember()
                        {
                            ActorNumber= 9,
                            DVDNumber = 11
                        },
                        new CastMember()
                        {
                            ActorNumber= 17,
                            DVDNumber = 12
                        },
                        new CastMember()
                        {
                            ActorNumber= 4,
                            DVDNumber = 13
                        },
                         new CastMember()
                        {
                            ActorNumber= 5,
                            DVDNumber = 14
                        },
                          new CastMember()
                        {
                            ActorNumber= 1,
                            DVDNumber = 15
                        },
                            new CastMember()
                        {
                            ActorNumber= 11,
                            DVDNumber = 15
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
                            DatePurchased = DateTime.ParseExact("2021-12-12","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 6,
                            DatePurchased = DateTime.ParseExact("2018-05-10","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 8,
                            DatePurchased = DateTime.ParseExact("2017-03-30","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 3,
                            DatePurchased = DateTime.ParseExact("2019-12-30","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 3,
                            DatePurchased = DateTime.ParseExact("2020-12-30","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                         new DVDCopy()
                        {
                            DVDNumber = 3,
                            DatePurchased = DateTime.ParseExact("2020-12-31","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 1,
                            DatePurchased = DateTime.ParseExact("2017-11-25","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 2,
                            DatePurchased = DateTime.ParseExact("2022-01-02","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 4,
                            DatePurchased = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                        new DVDCopy()
                        {
                            DVDNumber = 7,
                            DatePurchased = DateTime.ParseExact("2020-10-19","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                         new DVDCopy()
                        {
                            DVDNumber = 7,
                            DatePurchased = DateTime.ParseExact("2020-11-19","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 8,
                            DatePurchased = DateTime.ParseExact("2022-01-15","yyyy-MM-dd",null),
                        },
                           new DVDCopy()
                        {
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2022-03-11","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                            new DVDCopy()
                        {
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2022-03-11","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2022-03-11","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 9,
                            DatePurchased = DateTime.ParseExact("2020-07-15","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 10,
                            DatePurchased = DateTime.ParseExact("2020-02-15","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 11,
                            DatePurchased = DateTime.ParseExact("2022-03-13","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 12,
                            DatePurchased = DateTime.ParseExact("2020-11-15","yyyy-MM-dd",null),
                            IsLoan = false
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 13,
                            DatePurchased = DateTime.ParseExact("2020-12-05","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 13,
                            DatePurchased = DateTime.ParseExact("2020-10-15","yyyy-MM-dd",null),
                            IsLoan = true
                        },
                          new DVDCopy()
                        {
                            DVDNumber = 13,
                            DatePurchased = DateTime.ParseExact("2020-11-15","yyyy-MM-dd",null),
                            IsLoan = false
                        },

                          new DVDCopy()
                        {
                            DVDNumber = 14,
                            DatePurchased = DateTime.ParseExact("2020-10-15","yyyy-MM-dd",null),
                            IsLoan = true
                        },


                    });
                    context.SaveChanges();


                    //LoanType
                    if (!context.LoanTypes.Any())
                    {
                        context.LoanTypes.AddRange(new List<LoanType>()
                    {
                        new LoanType()
                        {
                            LoanTypeName = "Daily",
                            Duration = 1
                        },
                        new LoanType()
                        {
                            LoanTypeName = "Weekly",
                            Duration = 7
                        },
                        new LoanType()
                        {
                            LoanTypeName = "Monthly",
                            Duration = 30
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
                            MembershipCategoryName = "Silver",
                            MembershipCategoryDescription = "With a Silver membership, a member can rent 1 DVD copies at once.",
                            MembershipCategoryTotalLoans = 1,
                        },
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Gold",
                            MembershipCategoryDescription = "With a Gold membership, a member can rent 2 DVD copies at once.",
                            MembershipCategoryTotalLoans = 2,
                        },
                        new MembershipCategory
                        {
                            MembershipCategoryName = "Platinum",
                            MembershipCategoryDescription = "With a Platinum membership, a member can rent 3 DVD copies at once.",
                            MembershipCategoryTotalLoans = 3,
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
                            MemberFirstName = "Asha",
                            MemberLastName = "Dahal",
                            MemberAddress = "Bouddha, Kathmandu, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("1998-12-04","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 2,
                            MemberFirstName = "Om",
                            MemberLastName = "Thapa",
                            MemberAddress = "New Road, Kathmandu, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2000-09-08","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Garima",
                            MemberLastName = "Adhikari",
                            MemberAddress = "Kamalpokhari, Kathmandu, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2001-11-09","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Aditi",
                            MemberLastName = "Shrestha",
                            MemberAddress = "Bansbari, Biratnagar, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2006-05-01","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 2,
                            MemberFirstName = "Nitesh",
                            MemberLastName = "Giri",
                            MemberAddress = "Ghantaghar, Chitwan, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2005-12-12","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 1,
                            MemberFirstName = "Aditya",
                            MemberLastName = "Karmacharya",
                            MemberAddress = "Pulchowk, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("1990-07-03","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Mina",
                            MemberLastName = "Poudel",
                            MemberAddress = "Patan Durbar, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2002-06-18","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 2,
                            MemberFirstName = "Aroja",
                            MemberLastName = "Shrestha",
                            MemberAddress = "Kuleshwor, Kathmandu, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2003-06-18","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 1,
                            MemberFirstName = "Rashika",
                            MemberLastName = "Pandit",
                            MemberAddress = "Patan, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("1999-06-18","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Suku",
                            MemberLastName = "Poudel",
                            MemberAddress = "Jawalakhel, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2000-06-18","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 2,
                            MemberFirstName = "Animesh",
                            MemberLastName = "Bashnet",
                            MemberAddress = "Pulchowk, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2001-06-18","yyyy-MM-dd",null),
                        },
                        new Member()
                        {
                            MemberCategoryNumber = 3,
                            MemberFirstName = "Avantika",
                            MemberLastName = "Rana",
                            MemberAddress = "Imadol, Lalitpur, Nepal",
                            MemberDateOfBirth = DateTime.ParseExact("2002-06-18","yyyy-MM-dd",null),
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
                            CopyNumber = 3,
                            MemberNumber = 2,
                            DateOut = DateTime.ParseExact("2021-04-04","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-05","yyyy-MM-dd",null),
                            DateReturn = DateTime.ParseExact("2022-04-05","yyyy-MM-dd",null),
                            ReturnAmount= 200,
                        },
                         new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 3,
                            MemberNumber = 12,
                            DateOut = DateTime.ParseExact("2022-01-04","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-01-05","yyyy-MM-dd",null),
                            DateReturn = DateTime.ParseExact("2022-01-05","yyyy-MM-dd",null),
                            ReturnAmount= 200,

                        },
                          new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 3,
                            MemberNumber = 9,
                            DateOut = DateTime.ParseExact("2022-05-06","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-12","yyyy-MM-dd",null),

                        },

                         new Loan()

                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 2,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-01-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-01-27","yyyy-MM-dd",null),
                            DateReturn = DateTime.ParseExact("2022-01-26","yyyy-MM-dd",null),
                            ReturnAmount = 500,
                        },

                        new Loan()

                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 2,
                            MemberNumber = 1,
                            DateOut = DateTime.ParseExact("2022-04-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-27","yyyy-MM-dd",null),

                        },




                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 1,
                            MemberNumber = 7,
                            DateOut = DateTime.ParseExact("2022-02-04","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-02-04","yyyy-MM-dd",null),
                            DateReturn = DateTime.ParseExact("2022-02-03","yyyy-MM-dd",null),
                            ReturnAmount= 400
                        },

                        new Loan()
                        {
                            LoanTypeNumber = 3,
                            CopyNumber = 1,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-05-04","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-06-04","yyyy-MM-dd",null),
                        },


                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 4,
                            MemberNumber = 3,
                            DateOut = DateTime.ParseExact("2022-03-20","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-03-27","yyyy-MM-dd",null),

                        },
                        new Loan()
                        {
                            LoanTypeNumber = 1,
                            CopyNumber = 5,
                            MemberNumber = 1,
                            DateOut = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-02","yyyy-MM-dd",null),

                        },
                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 13,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-04-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-04-08","yyyy-MM-dd",null),

                        },
                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 14,
                            MemberNumber = 4,
                            DateOut = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),


                        },
                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 15,
                            MemberNumber = 5,
                            DateOut = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),


                        },

                        new Loan()
                        {
                            LoanTypeNumber = 2,
                            CopyNumber = 17,
                            MemberNumber = 10,
                            DateOut = DateTime.ParseExact("2022-05-01","yyyy-MM-dd",null),
                            DateDue = DateTime.ParseExact("2022-05-08","yyyy-MM-dd",null),


                        },
                    });
                        context.SaveChanges();
                    }







                }
            }
        }

    }
}
