using FakeBank.BankAPI.Application.Model.Authentication;
using FakeBank.BankAPI.Identity.Model;
using FakeBank.BankAPI.Identity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Identity
{
    public static class IdentityServiceExtension
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOSettings>(configuration.GetSection("Auth0"));
            string domain = $"https://{configuration["Auth0:Domain"]}/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = $"{configuration["Auth0:Audience"]}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            });
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}
