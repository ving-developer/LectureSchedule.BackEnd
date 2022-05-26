using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LectureSchedule.Data.Configuration
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection ConfigureDbConnection(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<LectureScheduleContext>(
                context => context.UseSqlite(connectionString)
            );
            return services;
        }
    }
}