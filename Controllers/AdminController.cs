using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAdminsPresentList()
        {
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAdminsPresentJSON(AgentTaskModel model)
        {
            var a = _adminServices.getAdminPresentList(model);
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult UpdateDeliveryStatus(AgentTaskModel model)
        {
            var response = _adminServices.UpdateDeliveryStatusByAdmin(model);
            return Json(response);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAdminsHistoryList()
        {
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAdminsHistoryJSON(AgentTaskModel model)
        {
            var a = _adminServices.getAdminHistoryList(model);
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult AdminDashboard()
        {
            var claimData = HttpContext.GetClaimsData();
            if (claimData.Name != null)
            {
                ViewBag.Name = claimData.Name;
            }
            var data = _adminServices.AdminDashboardGraph();

            ViewBag.ChartLabels = JsonConvert.SerializeObject(data.Select(d => d.DayName).ToArray());
            ViewBag.TotalSeries = JsonConvert.SerializeObject(data.Select(d => d.TotalOrders).ToArray());
            ViewBag.InTransitSeries = JsonConvert.SerializeObject(data.Select(d => d.InTransitOrders).ToArray());
            ViewBag.DeliveredSeries = JsonConvert.SerializeObject(data.Select(d => d.DeliveredOrders).ToArray());

            ViewBag.TotalOrders = data.Sum(d => d.TotalOrders);
            ViewBag.InTransitCount = data.Sum(d => d.InTransitOrders);
            ViewBag.DeliveredCount = data.Sum(d => d.DeliveredOrders);

            return View(); 
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult DeleteByAdmin(AgentTaskModel model)
        {
            var a = _adminServices.deleteByAdmin(model);
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetOrderById(AgentTaskModel model)
        {
            var a = _adminServices.Getorder(model);
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult UpdateOrderByAdmin(AgentTaskModel model)
        {
            var a=_adminServices.updateOrderDataByAdmin(model);
        
            
                return Json(a);

        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult VendorRegistrationApprovalRequest()
        {
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public IActionResult VendorRegistrationApprovalRequestJSON()
        {
            var a = _adminServices.VendorRegistrationRequest(); 
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public IActionResult AccpetVendorRegistrationRequest(int tempCompanyId)
        {
            var a = _adminServices.AcceptVendorRegistrationRequest(tempCompanyId);
            return Json(a);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult AgentRegistrationApprovalRequest()
        {
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public IActionResult AgentRegistrationApprovalRequestJSON() 
        {
            var a = _adminServices.AgentRegistrationRequest();
            return Json(a);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult AcceptAgentRegistrationRequest(int tempAgentId)
        {
            var a = _adminServices.AcceptAgentRegistrationRequest(tempAgentId);
            return Json(a); 
        }
        //public IActionResult AdminDashboardGraph()
        //{
        //    var b = _adminServices.AdminDashboardGraph();
        //    TempData["TotalOrder"] = b.TotalOrder;
        //    TempData["LiveOrder"] = b.LiveOrder;
        //    TempData["CompletedOrder"] = b.CompletedOrder;
        //    return Json(b);
        //}
    }
}
