using ContactManager.Application.IdentityEntities;
using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesImplementation;
using ContactManager.Application.ServicesInterface;
using ContactManager.InfraStructure.DbContext;
using ContactManager.InfraStructure.RepoImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManager.UI.StartUpExtension
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //Configure Services
            services.AddScoped<IErrorHandleService, ErrorHandleService>();
            services.AddScoped<ICountriesGetterService, CountriesGetterServices>();
            services.AddScoped<ICountriesAdderServices, CountriesAdderServices>();
            services.AddScoped<ICountriesUpdateService, CountriesUpdaterServices>();
            services.AddScoped<ICountriesDeleteServices, CountriesDeleterServices>();





            //Configre Repo
            services.AddScoped<IErrorHandleRepo, ErrorHandleRepo>();
            services.AddScoped<ICountryRepository, CountryRepository>();




            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUsers, ApplicationRole>(
                //options => {
                //options.Password.RequiredLength = 5;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireDigit = false;
                //options.Password.RequiredUniqueChars = 3; //Eg: AB12AB (unique characters are A,B,1,2)
                //}
                )
            .AddEntityFrameworkStores<ApplicationDbContext>()

            .AddDefaultTokenProviders()

            .AddUserStore<UserStore<ApplicationUsers, ApplicationRole, ApplicationDbContext, Guid>>()

            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("NotAuthorized", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return !context.User.Identity.IsAuthenticated;
                    });
                });
            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Account/Login";
            });

            return services;
        }
    }
}
