using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    public class VendorDashboardController : Controller
    {
        private readonly IVendorDashServices _vendorDashServices;
        public VendorDashboardController (IVendorDashServices vendorDashServices)
        {
            _vendorDashServices = vendorDashServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCurrentVendorListView()
            {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            return View();
        }
        public IActionResult GetCurrentVendorListJSON(AgentTaskModel model)
        {
            var a = _vendorDashServices.GetVendorKoList(model);
            return Json(a);
        }
        public IActionResult GetPastVendorListView()
        {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            return View();
        }
        public IActionResult GetPastVendorListJSON(AgentTaskModel model)
        {
            var a = _vendorDashServices.GetVendorKoRecord(model);
            return Json(a);
        }
    }
}
