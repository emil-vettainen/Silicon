using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Identity;
using Presentation.WebApp.Configuration.AutoMapper;
using Presentation.WebApp.Configurations;
using Presentation.WebApp.Helpers;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRouting(r => r.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapperSettings));

builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();


builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;


}).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();


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