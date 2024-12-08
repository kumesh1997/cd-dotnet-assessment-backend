using Assessment.Api.CustomConfigurations;
using Assessment.Api.Managers;
using Assessment.Api.Middlewares;
using AutoMapper;
using DataAccess.DBContexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB service config
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Automapper config
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
 * Dependency Injection
 */
// Repositories
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Managers
builder.Services.AddScoped<IClassManager, ClassManager>();
builder.Services.AddScoped<ISubjectManager, SubjectManager>();
builder.Services.AddScoped<IStudentManager, StudentManager>();

// Register the environment name as a singleton service
builder.Services.AddSingleton(builder.Environment.EnvironmentName);

// Register custom health check
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("custom_health_check");

// Add services to the container.

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

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapHealthChecks("/api/v1/healthcheck");

// global error handler
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
