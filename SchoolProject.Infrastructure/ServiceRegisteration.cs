using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole<int>>(option =>
            {
                option.SignIn.RequireConfirmedEmail = true;

                //password settings
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredUniqueChars = 1;

                //lokout settings
                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;

                //user settings
                option.User.AllowedUserNameCharacters = "abcdefghjklmnopqrstuvwxyzQWERTYUIOPLKJHGFDSAZXCVBNM0123456789-_.=+#@!$%^&*(){}";
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
