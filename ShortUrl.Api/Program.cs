using Microsoft.EntityFrameworkCore;
using ShortUrl.Core;
using ShortUrl.Core.Interfaces;
using ShortUrl.Repository;
using ShortUrl.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ShortUrlContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(p =>
{
    p.AllowAnyHeader();
    p.AllowAnyMethod();
    p.AllowAnyOrigin();
    p.SetIsOriginAllowed(o => true);
});

app.UseAuthorization();

app.MapControllers();

app.Run();