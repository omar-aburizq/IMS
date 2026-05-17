using Application.Repositories;
using Application.Services.AuthService;
using Application.Services.CategoryService;
using Application.Services.CurrentUserService;
using Application.Services.InventoryTransactionService;
using Application.Services.OrderService;
using Application.Services.ProductService;
using Application.Services.RoleService;
using Application.Services.UserService;
using Infrastructuer.Context;
using Infrastructuer.Data;
using Infrastructuer.Repositories;
using Infrastructuer.Service.CurrentUserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext Registration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// JWT Registration 
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  // Install Microsoft.AspNetCore.Authentication.JwtBearer
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

// Swagger Registration
builder.Services.AddSwaggerGen(c =>  
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IMS API",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme  // Send AccessToken (Header) 
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Put **_ONLY_** your JWT Bearer token hera",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityReq = new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { } }
    };
    c.AddSecurityRequirement(securityReq);
});

// Dependency Injection Registration
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IRoleService), typeof(RoleService));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IInventoryTransactionService), typeof(InventoryTransactionService));

builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));


var app = builder.Build();


// Configure the HTTP request pipeline.

UserSeedData.UserSeed(app.Services); // Seed Data Registration 

app.UseSwagger();   // Swagger pipeline

app.UseSwaggerUI(); // Swagger pipeline

app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication pipeline

app.UseAuthorization(); // Authorization pipeline

app.MapControllers();

app.Run();

