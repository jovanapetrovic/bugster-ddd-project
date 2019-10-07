using Bugster.Business.Models;
using Bugster.Business.ProjectApplicationService;
using Bugster.Business.TagsApplicationService;
using Bugster.Business.UserApplicationService;
using Bugster.Data;
using Bugster.Repositories.ProjectRepository;
using Bugster.Repositories.TagRepository;
using Bugster.Repositories.UserRepository;
using Bugster.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bugster
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<BugsterDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectApplicationService, ProjectApplicationService>();
            services.AddScoped<ITaskAssigner, BugAutoAssigner>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagApplicationService, TagApplicationService>();

            TypeAdapterConfig<UserResponse, UpdateUserViewModel>
                .ForType()
                .Map(destination => destination.FirstName, source => source.FullName.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[0])
                .Map(destination => destination.LastName, source => source.FullName.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[1]);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
