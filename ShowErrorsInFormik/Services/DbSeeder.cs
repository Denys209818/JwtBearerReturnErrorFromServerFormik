using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShowErrorsInFormik.Constants;
using ShowErrorsInFormik.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Services
{
    public static class DbSeeder
    {
        public static void SeedRoles(this IApplicationBuilder app) 
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new AppRole
                    {
                        Name = Roles.ADMIN
                    }).Wait();

                    roleManager.CreateAsync(new AppRole
                    {
                        Name = Roles.USER
                    }).Wait();
                }
            }
        }
    }
}
