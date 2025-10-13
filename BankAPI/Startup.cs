using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Service;
using BankAPI.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankAPI
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(services =>
            {
                services.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BankAPI",
                    Version = "v2",
                    Description ="we were crazy enough to builda Bank API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Guilherme Ramos",
                        Email = "guilhermegv890@gmail.com",
                        Url= new Uri("https://github.com/guiorozimbo/Csharp-BankAPI")
                    }
                });
            });
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // Add DbContext and other services here as needed
            services.AddDbContext<DAL.YouBakingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BankDB")));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>

                {
                    var prefix = string.IsNullOrEmpty(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{prefix}/swagger/v2/swagger.json", "BankAPI v2");
                   // c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
                
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankAPI v1"));
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
