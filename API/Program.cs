using Application.Repositories;
using Application.Services.CategoryService;
using Application.Services.OrderService;
using Application.Services.ProductService;
using Application.Services.RoleService;
using Application.Services.UserService;
using Infrastructuer.Context;
using Infrastructuer.Data;
using Infrastructuer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connection Strings Registration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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


var app = builder.Build();


// Configure the HTTP request pipeline.

UserSeedData.UserSeed(app.Services); // Seed Data Registration 

app.UseSwagger();   // Swagger pipeline
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();