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
        public IActionResult UpdateDeliveryStatus(AgentTaskModel model)
        {
            var response = _adminServices.UpdateDeliveryStatusByAdmin(model);
            return Json(response);
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

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult DeleteByAdmin(AgentTaskModel model)
        {
            var a = _adminServices.deleteByAdmin(model);
            return Json(a);
        }
        public IActionResult GetOrderById(AgentTaskModel model)
        {
            var a = _adminServices.Getorder(model);
            return Json(a);
        }

        public IActionResult UpdateOrderByAdmin(AgentTaskModel model)
        {
            var a=_adminServices.updateOrderDataByAdmin(model);
        
            
                return Json(a);

        }
        public IActionResult VendorRegistrationApprovalRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VendorRegistrationApprovalRequestJSON()
        {
            var a = _adminServices.VendorRegistrationRequest(); 
            return Json(a);
        }

        [HttpPost]
        public IActionResult AccpetVendorRegistrationRequest(int tempCompanyId)
        {
            var a = _adminServices.AcceptVendorRegistrationRequest(tempCompanyId);
            return Json(a);
        }
        public IActionResult AgentRegistrationApprovalRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgentRegistrationApprovalRequestJSON() 
        {
            var a = _adminServices.AgentRegistrationRequest();
            return Json(a);
        }
        [HttpPost]
        public IActionResult AcceptAgentRegistrationRequest(int tempAgentId)
        {
            var a = _adminServices.AcceptAgentRegistrationRequest(tempAgentId);
            return Json(a); 
        }
    }
}
