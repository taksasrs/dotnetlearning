using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestAPI.Models;
using TestAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestAPI.Repository;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Configuration;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development
    // EnvironmentName = Environments.Production
});

//JWT Authen config
// builder.Services.AddAuthentication(cfg => {
//     cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(x => {
//     x.RequireHttpsMetadata = false;
//     x.SaveToken = false;
//     x.TokenValidationParameters = new TokenValidationParameters {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(
//             Encoding.UTF8
//             .GetBytes(configuration["ApplicationSettings:JWT_Secret"])
//         ),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         ClockSkew = TimeSpan.Zero
//     };
// });

//init connection string
builder.Services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Register repositories and services
builder.Services.AddHttpClient();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<MovieService>();

var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
builder.Services.AddSingleton(new RedisCacheService(redisConnectionString));
builder.Services.AddScoped<AuthService>();

// Register controllers
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

