using Bebrand.Infra.CrossCutting.Identity.Data;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Bebrand.Infra.CrossCutting.IoC;
using Bebrand.Infra.Data;
using Bebrand.Services.Api.Configuration;
using Bebrand.Services.Api.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetDevPack.Identity;
using NetDevPack.Identity.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bebrand.Domain.Models;

namespace Bebrand.Services.Api
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:token").Value);
            var domain = Configuration.GetSection("AppSettings:domain")?.Value;
            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<BebrandContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.WithOrigins("http://localhost:4200,https://crm.bbdv.xyz,http://crmapp.bebrand.tv");
                    });
            });

            services.Configure<Appsettings>(Configuration.GetSection("Appsettings"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // AutoMapper Settings
            services.AddAutoMapperConfiguration();
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            services.AddAuthentication(key, domain);

            // NetDevPack.Identity dependency
            services.AddAspNetUserConfiguration();


            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration();

            services.AddSwaggerConfiguration();
            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerSetup();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Bebrand_Uploads")),
                RequestPath = "/files"
            });
        }

    }

}
