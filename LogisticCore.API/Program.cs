using LogisticCore.Api.Services;
using LogisticCore.Application.CustomServices;
using LogisticCore.Core.Configurations;
using LogisticCore.Core.Helper;
using LogisticCore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services .AddDbContext<LogisticCoreContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("LogisticConnectionStr"));
        opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
var appSettingsSection = builder.Configuration.GetSection("AppSettings");

var origin = appSettingsSection.GetValue<string>("AllowedOrigins");
builder.Services.Configure<AppSettings>(appSettingsSection);

RegistrationService.Registrations(builder.Services);
AuthorizationService.Authentication(builder.Services, builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AuthorizationService.AddSwaggerGen(builder.Services);
builder.Services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);
builder.Services.AddCors(option =>
                               option.AddDefaultPolicy(policy =>
                               {
                                   policy /*.WithOrigins(origin)*/
                                         .AllowAnyOrigin()
                                         .AllowAnyMethod()
                                         .AllowAnyHeader();
                               }));

var app = builder.Build();
app.AddGlobalErrorHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
