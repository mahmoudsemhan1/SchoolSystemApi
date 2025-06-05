using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolSystem.Application.DTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.Services;
using SchoolSystem.Domain.Models;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Infrastructure.Repositories;
using SchoolSystem.Infrastructure.UnitOfWork;
using SchoolSystemApi.Seed;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

 // 1. Database
builder.Services.AddDbContext<SchoolSystemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// 2. Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SchoolSystemDbContext>()
    .AddDefaultTokenProviders();
// 3. JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

//add IUnitOfWork 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//add automapper 
// if the mapping in the same project 
//builder.Services.AddAutoMapper(typeof(MappingProfile));
//if i have some projects
//(layers -> appliaction,domain....)i use it, beacuse it search about the class that is name of it is=>(profile) and add  it  
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//add sevice for the table I will need 
//student
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentServices>();
//Teacher 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherServices,TeacherService>();
//Classes
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassServices,ClassServices>();
// TeacherClass
builder.Services.AddScoped<ITeacherClassService, TeacherClassService>();
//Token 
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers()
    .AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    x.JsonSerializerOptions.WriteIndented = true;
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Create a scope to resolve scoped services like RoleManager and run the role seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
