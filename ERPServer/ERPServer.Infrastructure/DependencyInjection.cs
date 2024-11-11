using ERPServer.Domain.Entities;
using ERPServer.Infrastructure.Context;
using ERPServer.Infrastructure.Options;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERPServer.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(con =>
        {
            con.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<AppUser, IdentityRole<Guid>>(cfr =>
        {
            cfr.Password.RequiredLength = 6;
            cfr.Password.RequireNonAlphanumeric = false;
            cfr.Password.RequireUppercase = false;
            cfr.Password.RequireLowercase = false;
            cfr.Password.RequireDigit = true;
            cfr.SignIn.RequireConfirmedEmail = true;
            cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            cfr.Lockout.MaxFailedAccessAttempts = 3;
            cfr.Lockout.AllowedForNewUsers = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.ConfigureOptions<JwtTokenOptionsSetup>();
        services.AddAuthentication()
            .AddJwtBearer();
        services.AddAuthorizationBuilder();

        services.Scan(action =>
        {
            action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: Scrutor.RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        return services;
    }
}
