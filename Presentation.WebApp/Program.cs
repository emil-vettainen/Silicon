using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WebApp.ViewModels;
using Shared.Utilis;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(r => r.LowercaseUrls = true);

builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ProfileRepository>();
builder.Services.AddSingleton<ErrorLogger>(new ErrorLogger(@"C:\CSharp\Silicon\log.txt"));


var app = builder.Build();



app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();