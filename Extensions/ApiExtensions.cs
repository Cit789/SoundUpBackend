using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SoundUp.Infrastructure;
using System.Text;

namespace SoundUp.Extensions
{
   static public class ApiExtensions
    {
        
        public static void AddApiAuthentication(this IServiceCollection services,IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                        ClockSkew = TimeSpan.Zero,
                    };
                    options.Events = new JwtBearerEvents
                    { 
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["cookie"];
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        } 
    }
}
