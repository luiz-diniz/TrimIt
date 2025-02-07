using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ShortUrl.Api.Extensions;
using System.Text;

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

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            builder.Configuration.GetValue<string>("Authentication:SecretKey")))
    };
});

//builder.Services.AddAuthorization(opts =>
//{
//    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseIpRateLimiting();
app.Run();