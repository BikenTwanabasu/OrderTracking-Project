using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CollegeProject.RepoClass
{
    public class ClaimsRequirementFilter:IAuthorizationFilter
    {
        private readonly string[] _requiredClaims;

        public ClaimsRequirementFilter(string[] requiredClaims)
        {
            _requiredClaims = requiredClaims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims =context.HttpContext.User.Claims;

            if(_requiredClaims.Any(role =>!userClaims.Any(c=>c.Type==ClaimTypes.Role && c.Value == role))){
                context.Result = new ForbidResult();
            }


        }
    }
}
