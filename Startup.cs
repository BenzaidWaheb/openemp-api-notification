using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenempApiNotifications.Models;

namespace OpenEMP_NotificationAPI
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            services.AddEntityFrameworkNpgsql().AddDbContext<NotificationDbContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("NotificationDbContext")));
            //services.AddDbContext<NotificationDbContext>(
            //    opt => opt.UseNpgsql(
            //        @"Host=localhost;
            //        Initial Catalog=NotificationDB;Integrated Security=True;
            //        Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
            //        ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            //    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
