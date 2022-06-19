using LectureSchedule.Domain;
using LectureSchedule.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LectureSchedule.Data.Context
{
    public class LectureScheduleContext : IdentityDbContext<User,Role,int,
                                                            IdentityUserClaim<int>,
                                                            UserRole,
                                                            IdentityUserLogin<int>,
                                                            IdentityRoleClaim<int>,
                                                            IdentityUserToken<int>>
    {
        public LectureScheduleContext(DbContextOptions<LectureScheduleContext> options) : base(options) { }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<TicketLot> TicketLots { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SpeakerLecture> SpeakerLectures { get; set; }

        public DbSet<PublicityCampaign> PublicityCampaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Specifying that there will be an N:N relationship between the User and Roles table,
            //where the UserRoles table must contain the foreign keys of both tables
            modelBuilder.Entity<UserRole>(usrRole =>
                {
                    usrRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                    usrRole.HasOne(ur => ur.Role)
                           .WithMany(r => r.UserRoles)
                           .HasForeignKey(ur => ur.RoleId)
                           .IsRequired();

                    usrRole.HasOne(ur => ur.User)
                           .WithMany(r => r.UserRoles)
                           .HasForeignKey(ur => ur.UserId)
                           .IsRequired();
                }
            );
            //Double primary key, the EntityFramework will understand that this table links the two tables with the N:N relationship
            modelBuilder.Entity<SpeakerLecture>()
                .HasKey(sp => new { sp.LectureId, sp.SpeakerId });
        }
    }
}
