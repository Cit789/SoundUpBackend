
using Microsoft.EntityFrameworkCore;
using SoundUp;
using SoundUp.Extensions;
using SoundUp.Infrastructure;
using SoundUp.Interfaces.Auth;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
builder.Services.AddApiAuthentication(builder.Configuration);

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
});

builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();


var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
