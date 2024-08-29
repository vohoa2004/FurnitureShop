using AutoMapper;
using FurnitureShop.BLL.Services;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.BLL.Utils;
using FurnitureShop.DAL;
using FurnitureShop.DAL.Repositories;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args); // entry point for the project

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("app-cors",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination")
            .AllowAnyMethod();
        });
});

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

// Database
builder.Services.AddDbContext<FurnitureShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

// auto mapper
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

// Dependency Injections for Services and Repos
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IFurnitureRepository, FurnitureRepository>();
builder.Services.AddScoped<IFurnitureService, FurnitureService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("app-cors");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
