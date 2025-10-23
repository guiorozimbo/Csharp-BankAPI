using AutoMapper;
using BankAPI.DAL;
using BankAPI.Profiles;
using BankAPI.Service;
using BankAPI.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BankAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<YouBakingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(typeof(AutomapperProfiles));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BankAPI",
                    Version = "v2",
                    Description = "We were crazy enough to build a Bank API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Guilherme Ramos",
                        Email = "guilhermegv890@gmail.com",
                        Url = new Uri("https://github.com/guiorozimbo/Csharp-BankAPI")
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "BankAPI v2");
                });
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
