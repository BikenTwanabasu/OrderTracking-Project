using CollegeProject.Models;
using System.Security.Claims;

namespace CollegeProject.RepoClass
{
    public static class ClaimsData
    {
       public static LogInInfoClaims GetClaimsData(this HttpContext context)
        {
            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var model= new LogInInfoClaims();

            if (claimsIdentity != null)
            {
                model.Name = claimsIdentity.FindFirst(x => x.Type == "Name").Value;
                model.Email = claimsIdentity.FindFirst(x => x.Type == "Email").Value;
                model.Id = claimsIdentity.FindFirst(x => x.Type == "Id").Value;
                model.Address = claimsIdentity.FindFirst(x => x.Type == "Address").Value;
                model.Address = claimsIdentity.FindFirst(x => x.Type == "AdminName").Value;
                model.Address = claimsIdentity.FindFirst(x => x.Type == "AdminId").Value;
                model.Address = claimsIdentity.FindFirst(x => x.Type == "AdminEmail").Value;
                model.Address = claimsIdentity.FindFirst(x => x.Type == "isSuperAdmin").Value;
            }
            return model;
        }
    }
}
