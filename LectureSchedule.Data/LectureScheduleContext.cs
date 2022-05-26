using LectureSchedule.Domain;
using Microsoft.EntityFrameworkCore;

namespace LectureSchedule.Data
{
    public class LectureScheduleContext : DbContext
    {
        public LectureScheduleContext(DbContextOptions<LectureScheduleContext> options) : base(options) {}

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<TicketLot> TicketLots { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SpeakerLecture> SpeakerLectures { get; set; }

        public DbSet<PublicityCampaign> PublicityCampaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Double primary key, the EntityFramework will understand that this table links the two tables with the N:N relationship
            modelBuilder.Entity<SpeakerLecture>()
                .HasKey(sp => new { sp.LectureId, sp.SpeakerId});
        }
    }
}
