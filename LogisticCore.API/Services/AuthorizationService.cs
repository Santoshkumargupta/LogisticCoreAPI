using LogisticCore.Core.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LogisticCore.Api.Services
{
    public static class AuthorizationService
    {

        public static void Authentication(IServiceCollection services, ConfigurationManager configuration)
        {
            
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = appSettings.Issuer,
                    ValidateIssuer = false,
                    //ValidAudience = appSettings.Audience,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        c.Response.ContentType = "application/json";
                        var serializeOptions = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        };
                        string responseMsg = JsonConvert.SerializeObject("The access token provided is not valid.");

                        
                         if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            c.Response.Headers.Add("Token-Expired", "true");
                            responseMsg = "The access token provided has expired.";
                            //JsonConvert.SerializeObject("");
                        }
                        var response = JsonSerializer.Serialize(
                                   new Core.Model.ApiResponse<Object>
                                   {
                                       StatusCode = (int)StatusCodes.Status401Unauthorized,
                                       ErrorMessage = responseMsg
                                   }, serializeOptions);

                        c.Response.WriteAsync(response).Wait();
                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });


        }

        public static void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "LogisticCore API", Version = "v1" });
                //option.OperationFilter<FileUploadFilter>(); 
                //option.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"

                });
                //option.OperationFilter<AddRequiredHeaderParameter>();
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                         {
                          Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                          }
                         },
                         new string[]{ }
                    }
                });
             
            });
        }
    }
}
