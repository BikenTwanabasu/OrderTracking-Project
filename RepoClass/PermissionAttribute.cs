using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.RepoClass
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(params string[] roles) : base(typeof(ClaimsRequirementFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
