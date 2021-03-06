using LectureSchedule.Data.Configuration;
using LectureSchedule.Data.Persistence;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Service;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace LectureSchedule.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add configurations
            services.ConfigureDbConnection(Configuration.GetConnectionString("Default"));
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(
                x => x.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());
            //Add dependency injection
            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ILectureService, LectureService>()
                .AddScoped<ITicketLotService, TicketLotService>()
                .AddScoped<IUploadService, UploadService>();
            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LectureSchedule.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LectureSchedule.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors( policy => policy.AllowAnyOrigin()
                                         .AllowAnyHeader()
                                         .AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/resources")
            });
        }
    }
}
