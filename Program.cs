using Microsoft.EntityFrameworkCore;
using SoundUp;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
});

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


app.MapControllers();

app.Run();
