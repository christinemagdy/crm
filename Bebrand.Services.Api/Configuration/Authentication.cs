using Bebrand.Domain.Enums;
using Bebrand.Infra.CrossCutting.Identity.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Configuration
{
    public static class Authentication
    {
        public static void AddAuthentication(this IServiceCollection services, byte[] key, string Domain)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            //ValidIssuer = Domain,
                            //ValidAudience = Domain,
                            ClockSkew = TimeSpan.FromDays(1) // remove delay of token when expire
                        };

                        // We have to hook the OnMessageReceived event in order to
                        // allow the JWT authentication handler to read the access
                        // token from the query string when a WebSocket or 
                        // Server-Sent Events request comes in.
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/notificationHub")))
                                {
                                    // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            services.AddAuthorization(config =>
            {
                config.AddPolicy("ShouldContainRole",
                    options => options.RequireClaim("SuperAdmin"));
            });
            services.AddAuthorization(options =>
            {


                options.AddPolicy("RoleBasedClaim", policy => policy.RequireClaim("ManagerPermissions", "true"));
            });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SalesDirectorData", policy => policy.Requirements.Add(new ClaimRequirement("SalesDirector", "All")));
            //    // Add a new Policy with requirement to check for Admin
            //    options.AddPolicy("ShouldBeAnAdmin", options =>
            //    {
            //        options.RequireAuthenticatedUser();
            //        options.AuthenticationSchemes.Add(
            //                JwtBearerDefaults.AuthenticationScheme);
            //        options.Requirements.Add(new ShouldBeAnAdminRequirement());
            //    });


            //    //options.AddPolicy("CanRemoveCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Remove")));
            //    //options.AddPolicy("CanWriteProductData", policy => policy.Requirements.Add(new ClaimRequirement("Products", "Write")));
            //    //options.AddPolicy("CanRemoveProductData", policy => policy.Requirements.Add(new ClaimRequirement("Products", "Remove")));
            //    //options.AddPolicy("CanWriteSupplierData", policy => policy.Requirements.Add(new ClaimRequirement("Suppliers", "Write")));
            //    //options.AddPolicy("CanRemoveSupplierData", policy => policy.Requirements.Add(new ClaimRequirement("Suppliers", "Remove")));
            //    //options.AddPolicy("CanWriteOpportunityData", policy => policy.Requirements.Add(new ClaimRequirement("Opportunities", "Write")));
            //    //options.AddPolicy("CanRemoveOpportunityData", policy => policy.Requirements.Add(new ClaimRequirement("Opportunities", "Remove")));

            //});
        }
    }
}
