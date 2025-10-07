using BillEase360_CodeFirstApproach.BusinessUsers.Infrastructure;
using BillEase360_CodeFirstApproach.Invoice.Infrastructure;
using BillEase360_CodeFirstApproach.Products.Infrastructure;
using BillEase360_CodeFirstApproach.Users.Application.Helpers;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using BillEase360_CodeFirstApproach.Users.Infrastructure;
using BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


// Users DbContext (default)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IUserRolerepository,RolesRepository>();
builder.Services.AddScoped<RolesService>();

builder.Services.AddScoped<IUserPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<PermissionService>();

builder.Services.AddScoped<IUserRoleIdRepository, UserRoleIdRepository>();
builder.Services.AddScoped<UserRoleService>();

builder.Services.AddScoped<IJwtService, JwtService>();  // ✅ Must be here
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IUserRolePermissionRepository, UserRolePermissionRepository>();
builder.Services.AddScoped<RolePermissionService>();

// Products DbContext (separate)
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Business Users BbContext(seperate)
builder.Services.AddDbContext<BusinessUserDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Invoices DbContext(Seperate)
builder.Services.AddDbContext<InvoiceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

// Add Authentication + JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true, // ensures token hasn't expired
        ClockSkew = TimeSpan.Zero // no extra time beyond expiry
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

