using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.BL.Repository;
using RepositoryPatternWithUOW.BL.UnitOfWork;
using RepositoryPatternWithUOW.core.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connectionString));

 
//builder.Services.AddScoped(typeof(IBaseRepository<>),typeof (BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
