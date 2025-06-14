using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollegeProject.Controllers
{
    public class LogController : Controller
    {
        private IServices _services;

        public LogController(IServices services)
        {
            _services = services;
        }
        
        public IActionResult AgentLoggingIn(Agent agent)
        {
            var a = _services.AgentLogIn(agent);
            if (a.ResponseCode!=null)
            {
                var claim = new List<Claim>
                {
                    new Claim("Name",a.AgentName),
                    new Claim("Email",a.AgentEmail),
                    new Claim("Address",a.AgentAddress),
                    new Claim("Id",a.AgentId),
                    new Claim(ClaimTypes.Role,"Agent")
                    
                };
                var ClaimIdentity =new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme ,new ClaimsPrincipal(ClaimIdentity));
                return Json(a);
            }
            return View();
        } 
        
        public IActionResult VendorLoggingIn(Vendor vendor)
        {
            var a = _services.VendorLogIn(vendor);
            if (a.ResponseCode == 0) 
            {
                return Json(a);
            }
            if (a.ResponseCode!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Name",a.CompanyName),
                    new Claim("Id",a.CompanyId),
                    new Claim("Email",a.CompanyEmail),
                    new Claim("Address",a.CompanyAddress),
                    new Claim(ClaimTypes.Role,"Vendor")
                };
                var claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);    
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                return Json(a);
            }
            return View();
        }
        public IActionResult AdminLoggingIn(Admin admin)
        {
            var a = _services.AdminLogIn(admin);
            if (a.ResponseCode == 0 && a.AdminEmail != null)
            {
                return Json(a);
            }
            else if (a.ResponseCode != 0)
            {
                int isSuperAdmin = 0;
                string Role = "Admin"; 
                if(a._isSuperAdmin) 
                {
                     isSuperAdmin = 1;
                    Role = "SuperAdmin";
                }
                
                var claims = new List<Claim>
                {
                    new Claim("Name",a.AdminName),
                    new Claim("Id",a.AdminId),
                    new Claim("Email",a.AdminEmail),
                    new Claim(ClaimTypes.Role,Role)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Json(a);
            }
            return View();
        }

        public IActionResult Index()
        {
                return View();
        }

        public async Task<IActionResult> Logout()
        {
            var ClaimData = HttpContext.User;
            if(ClaimData != null) {
                var Role = ClaimData.FindFirst(ClaimTypes.Role);

                if (Role.Value == "Agent")
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("AgentLoggingIn", "Log");
                }
                else if (Role.Value == "Vendor")
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("VendorLoggingIn", "Log");

                }
                else if (Role.Value == "Admin")
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("AdminLoggingIn", "Log");

                }
                else if (Role.Value == "SuperAdmin")
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("AdminLoggingIn", "Log");

                }
            }
            return RedirectToAction("Index","Log");

            
            
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

       
    }
}

