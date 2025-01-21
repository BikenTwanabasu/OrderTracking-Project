using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    public class ProjectController : Controller
    {
        private IServices _services;

        public ProjectController(IServices services)
        {
            _services = services;
        }
      
        public IActionResult VendorRegistration(Vendor vendor)
        {
            
            var a=_services.RegisterVendor(vendor);
            if (a.CompanyEmail!=null)
            {
                return Json(a);
            }
            return View();

        }
     
        public IActionResult AgentRegistration(Agent agent)
        {

            var a = _services.RegisterAgent(agent);
            if (a.AgentEmail!=null)
            {
                return Json(a);
            }
            return View();

        }
        [Permission("Vendor")]
        public IActionResult OrderCreation(OrderAndStudentModel orderAndStudentModel)
        {

            var a = _services.OrderCreations(orderAndStudentModel);
            if (a)
            {
                
               
                
                return Json(a);
            }
            var Info = HttpContext.GetClaimsData();
            ViewBag.I = Info.Id;
            ViewBag.E = Info.Email;
            return View();

        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult CustOrderTracker()
        {
            return View();
        }
        public IActionResult TrackTheOrder(AgentTaskModel agent)
        {
            var a =_services.getStatusByOrderID(agent);
            return Json(a);
        }

        public IActionResult FirstPage()
        {
            return View();
        }
    }
}
