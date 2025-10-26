using System.Text;
using Application.Helpers;
using Application.ImageUploader;
using Application.Interfaces;
using Application.IServices.IAuthServices;
using Application.IServices.ICategoryServices;
using Application.IServices.IEmailService;
using Application.IServices.IProductServices;
using Application.IServices.IRoleServices;
using Application.IServices.ITwilioServices;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services.AuthServices;
using Infrastructure.Services.CategoryServices;
using Infrastructure.Services.EmailService;
using Infrastructure.Services.mageUploader;
using Infrastructure.Services.ProductServices;
using Infrastructure.Services.RoleServices;
using Infrastructure.Services.TwilioServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        #endregion

        #region Application Services
        services.AddTransient<ITwilioService, TwilioService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IImageUploader, ImageUploader>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        #endregion

        #region Configuration Bindings
        services.Configure<JwtSettings>(configuration.GetSection("Authentication:Jwt"));
        services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
        services.Configure<TwilioSettings>(configuration.GetSection("TwilioConfig"));
        services.Configure<URLs>(configuration.GetSection("URLs"));
        #endregion

        #region Authentication & Authorization
        var key = Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Secret"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Authentication:Jwt:Issuer"],
                ValidAudience = configuration["Authentication:Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });
        #endregion

        return services;
    }
}
