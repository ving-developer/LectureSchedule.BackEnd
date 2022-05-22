using LectureSchedule.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LectureSchedule.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Lecture> Lectures { get; set; }
    }
}
