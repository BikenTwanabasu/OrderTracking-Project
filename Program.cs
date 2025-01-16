using collegeproject.repoclass;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IServices,Services>();
builder.Services.AddSingleton<Iagentdashservices, agentdashservices>();
builder.Services.AddSingleton<IVendorDashServices, VendorDashServices>();
builder.Services.AddSingleton<IAdminServices, AdminServices>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                     options.LoginPath = "/Log/AgentLoggingIn";
                     options.LogoutPath = "/Project/AgentRegistration";
                     //options.AccessDeniedPath = "/Account/UserAccessDenied";
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
    pattern: "{controller=Log}/{action=VendorLoggingIn}/{id?}");

app.Run();
