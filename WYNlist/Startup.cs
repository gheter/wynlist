using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Wynlist.Data;
using Wynlist.Data.Entities;
using Wynlist.Services;

namespace Wynlist
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _env = env;
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<WynUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true; 

            })
            .AddEntityFrameworkStores<WynlistContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });


            services.AddDbContext<WynlistContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("WynlistConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<IMailService, NullMailService>();
            //Support for real mail service

            services.AddTransient<WynlistSeeder>();

            services.AddScoped<IWynlistRespository, WynlistRespository>();

            services.AddMvc(opt =>
                {
                    if (_env.IsProduction())
                    {
                        opt.Filters.Add(new RequireHttpsAttribute());
                    }
                })
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            // app.UseDefaultFiles(); //Looks for files in wwwroot and points to that directory
            
            app.UseStaticFiles();

            app.UseAuthentication();

            //Using MVC instead of DEFAULT
            app.UseMvc(cfg =>
                {
                    cfg.MapRoute("Default",
                       "{controller}/{action}/{id?}",
                       new { Controller = "App", Action = "Index" });
                });

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<WynlistSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}