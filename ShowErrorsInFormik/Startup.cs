using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShowErrorsInFormik.Data;
using ShowErrorsInFormik.Data.Identity;
using ShowErrorsInFormik.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.IO;

namespace ShowErrorsInFormik
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
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "frontend/build";
            });

            services.AddDbContext<EFDataContext>((DbContextOptionsBuilder builder) => {
                builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IJwtBearerTokenService, JwtBearerTokenService>();

            services.AddIdentity<AppUser, AppRole>((IdentityOptions opts) => {
                opts.Password.RequireDigit = false;
                opts.Password.RequiredLength = 5;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<EFDataContext>()
            .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(MyAutoMapper));

            services.AddSwaggerGen((SwaggerGenOptions opts) => {
                opts.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "ShowErrorsInFormik",
                    Version = "v1"
                });

                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
                    Scheme = "bearer",
                    Description = "description of my website",
                    Type = SecuritySchemeType.Http
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement {{
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        
                    },
                    new List<string>()
                }});
            });

            services.AddFluentValidation(conf => 
            conf.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI((SwaggerUIOptions opts) => {
                    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "My application");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(dirPath)) 
            {
                Directory.CreateDirectory(dirPath);
            }

            app.UseStaticFiles(new StaticFileOptions { 
                RequestPath = "/images",
                FileProvider = new PhysicalFileProvider(dirPath)
            });

            app.SeedRoles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "frontend";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
