﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service;
using WebPizza_API_BackEnd.Service.IService;

var builder = WebApplication.CreateBuilder(args);
//cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});//

// Đọc chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



// Thêm DbContext vào DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký dịch vụ Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IToppingService, ToppingService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPromotionService,PromotionService>();
// Đăng lớp Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepo, ProductRepository>();
builder.Services.AddScoped<ISizeRepo, SizeRepository>();
builder.Services.AddScoped<ITopingRepo, ToppingRepository>();
builder.Services.AddScoped<IPromotionRepo, PromotionRepository>();
builder.Services.AddScoped<IProductPromotionRepo, ProductPromotionRepository>();
//Cấu hình xác thực email



// Cấu hình xác thực JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Thêm Authorization
builder.Services.AddAuthorization();

// Thêm các dịch vụ khác
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll"); 
app.UseHttpsRedirection();
app.UseAuthentication(); // 🔥 Bắt buộc phải có nếu dùng JWT
app.UseAuthorization();

app.MapControllers();
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    context.Database.Migrate(); // Đảm bảo DB cập nhật
//    DbInitializer.SeedData(context); // Gọi seed data
//}
app.Run();
