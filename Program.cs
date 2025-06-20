using collegeproject.repoclass;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Authentication.Cookies;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IServices,Services>();
builder.Services.AddSingleton<Iagentdashservices, agentdashservices>();
builder.Services.AddSingleton<IVendorDashServices, VendorDashServices>();
builder.Services.AddSingleton<IAdminServices, AdminServices>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
                     options.LoginPath = "/Log/AgentLoggingIn";
                     options.LogoutPath = "/Log/Logout";
                     options.AccessDeniedPath = "/Log/AccessDenied";
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
    pattern: "{controller=Project}/{action=FirstPage}/{id?}");

app.Run();
