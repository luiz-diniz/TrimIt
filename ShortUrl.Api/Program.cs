using AspNetCoreRateLimit;
using ShortUrl.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(x =>
{
    x.TimestampFormat = "[HH:mm:ss] ";
});

builder.Configuration.AddEnvironmentVariables();

builder.InitializeApplicationDependencies();

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
app.UseIpRateLimiting();
app.Run();