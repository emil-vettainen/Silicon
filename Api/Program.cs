using Business.Services.Api;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Api;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApiDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Api")));

builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseRepository>();


builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<AuthorRepository>();

builder.Services.AddSingleton<ErrorLogger>(new ErrorLogger(@"C:\CSharp\Silicon\log.txt"));


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
