using Microsoft.EntityFrameworkCore;
using Web_Pizza.Configurations;
using Web_Pizza.Entities;
using Web_Pizza.Repositories;
using Web_Pizza.Services.IServices;
using Web_Pizza.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ??ng ký EmailSettings t? appsettings.json
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// ??ng ký PizzaStoreContext v?i DI container
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaStoreConnection")));

// ??ng ký c?u hình JWT t? appsetting.json
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));

// Add services to the container.

// ??ng ký các d?ch v? vào container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

//C?u hình Authentication v?i JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer, // L?y t? JwtConfiguration
            ValidAudience = jwtConfig.Audience, // L?y t? JwtConfiguration
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)) // L?y t? JwtConfiguration
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
