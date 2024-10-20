using Microsoft.EntityFrameworkCore;
using Company.Data.Context;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.service.Interfaces;
using Company.service.Services;
using Company.service.Mapping;
using Company.Data.Models;
using Microsoft.AspNetCore.Identity;
using Company.service.Helper;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Company2DbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));

builder.Services.Configure<EmailData>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<TwillioSettings>(builder.Configuration.GetSection("Twillio"));
builder.Services.AddTransient<ISMSService, SMSService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddGoogle(options =>
{
    IConfiguration googleAuth = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleAuth["ClientId"];
    options.ClientSecret = googleAuth["ClientSecret"];
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.Password.RequiredUniqueChars = 2;
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = true;
    config.Password.RequiredLength = 6;
    config.User.RequireUniqueEmail = true;
    config.Lockout.AllowedForNewUsers = true;
    config.Lockout.MaxFailedAccessAttempts = 3;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
}).AddEntityFrameworkStores<Company2DbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "CompanyAdmin";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignUP}/{id?}");

app.Run();
