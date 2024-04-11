using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SqlRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WebApp.Configuration.AutoMapper;
using Presentation.WebApp.Helpers;
using Presentation.WebApp.ViewModels;
using Shared.Utilis;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(r => r.LowercaseUrls = true);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapperSettings));

builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Test")));
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;


})
    .AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/signin";
    options.LogoutPath = "/signout";
    options.AccessDeniedPath = "/denied";

    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});


builder.Services.AddAuthentication()
    .AddGoogle(GoogleOptions => 
    {
        GoogleOptions.ClientId = "893316874513-a6a0bnpavt6tj1262db69jp4rpkasq4l";
        GoogleOptions.ClientSecret = "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";
    })
    .AddFacebook( FacebookOptions =>
    {
        FacebookOptions.AppId = "276230208847426";
        FacebookOptions.AppSecret = "fa8c8957ec6c242f81323a3453e31455";

    });


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();

builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<UserAddressRepository>();
builder.Services.AddScoped<OptionalAddressRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<SavedCourseRepository>();

builder.Services.AddScoped<UploadService>();


builder.Services.AddSingleton<ErrorLogger>(new ErrorLogger(@"C:\CSharp\Silicon\log.txt"));




var app = builder.Build();



app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UserSessionValidation();
app.UseAuthorization();



using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["Admin", "User"];
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Home}/{id?}");
app.Run();