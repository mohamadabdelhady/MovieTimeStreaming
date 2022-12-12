global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
// global using MovieTimeStreaming.Models;
global using MovieTimeStreaming.Data;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
});
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = true
    )
    .AddEntityFrameworkStores<ApplicationDbContext>() .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);;

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Admin/AccessDenied";
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToFolder("/Auth");
    options.Conventions.AuthorizeFolder("/User");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapControllers();
app.Run();
