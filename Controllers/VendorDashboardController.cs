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
        [Permission("Vendor")]
        public IActionResult Index()
        {
            return View();
        }
        [Permission("Vendor")]
        public IActionResult GetCurrentVendorListView()
            {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            return View();
        }
        [Permission("Vendor")]
        public IActionResult GetCurrentVendorListJSON(AgentTaskModel model)
        {
            var a = _vendorDashServices.GetVendorKoList(model);
            return Json(a);
        }
        [Permission("Vendor")]
        public IActionResult GetPastVendorListView()
        {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            return View();
        }
        [Permission("Vendor")]
        public IActionResult GetPastVendorListJSON(AgentTaskModel model)
        {
            var a = _vendorDashServices.GetVendorKoRecord(model);
            return Json(a);
        }
        public IActionResult VendorDashboard()
        {
            return View();
        }

        public IActionResult DeleteOrderByVednor(AgentTaskModel model)
        {
            var a = _vendorDashServices.DeleteOrderByVendor(model);
            return Json(a);

        }
    }
}
