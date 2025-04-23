using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.DTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.Services;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Infrastructure.Repositories;
using SchoolSystem.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SchoolSystemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
//
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<ClassServices>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
