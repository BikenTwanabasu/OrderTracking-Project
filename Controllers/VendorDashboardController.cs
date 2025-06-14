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
            var a = HttpContext.GetClaimsData();
            ViewBag.Name = a.Name;
            VendorDashboardGraph();
            ViewBag.ChartLabels = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            var TotalOrder = TempData["TotalOrder"];
            var LiveOrder = TempData["LiveOrder"];
            var CompletedOrder = TempData["CompletedOrder"];
            ViewBag.TotalSeries = TotalOrder;
            ViewBag.LiveSeries = LiveOrder;
            ViewBag.PastSeries = CompletedOrder;
            return View();
        }

        public IActionResult DeleteOrderByVednor(AgentTaskModel model)
        {
            var a = _vendorDashServices.DeleteOrderByVendor(model);
            return Json(a);

        }
        public IActionResult VendorDashboardGraph()
        {
            var a = HttpContext.GetClaimsData();
            var b = _vendorDashServices.VendorDashboardGraph(Convert.ToInt32(a.Id));
            TempData["TotalOrder"] = b.TotalOrder;
            TempData["LiveOrder"] = b.LiveOrder;
            TempData["CompletedOrder"] = b.CompletedOrder;
            return Json(b);
        }
    }
}
