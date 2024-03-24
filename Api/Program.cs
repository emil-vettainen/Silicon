using Business.Services.Api;
using Business.Services.SubscriberServices;
using Infrastructure.Contexts;
using Infrastructure.Helper;
using Infrastructure.Repositories.Api;
using Infrastructure.Repositories.MongoRepositories;
using Infrastructure.Repositories.SqlRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
builder.Services.AddScoped<SubscribeService>();
builder.Services.AddScoped<SubscribeRepository>();


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<MongoRepository>();


builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(Business.Helper.AutoMapper).Assembly);






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
