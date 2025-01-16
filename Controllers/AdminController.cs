using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAdminsPresentList()
        {
            return View();
        }
        public IActionResult GetAdminsPresentJSON(AgentTaskModel model)
        {
            var a = _adminServices.getAdminPresentList(model);
            return Json(a);
        }
        public IActionResult GetAdminsHistoryList()
        {
            return View();
        }
        public IActionResult GetAdminsHistoryJSON(AgentTaskModel model)
        {
            var a = _adminServices.getAdminHistoryList(model);
            return Json(a);
        }
    }
}
