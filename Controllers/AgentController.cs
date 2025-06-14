using CollegeProject.Models;
using CollegeProject.RepoClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace CollegeProject.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly Iagentdashservices _agentdashservices;
        private IConnectionMultiplexer _redis;

        public AgentController(Iagentdashservices agentdashservices,IConnectionMultiplexer redis)
        {
            _agentdashservices = agentdashservices;
            _redis = redis;
        }
        [Permission("Agent")]

        public IActionResult Index()
        {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            ViewBag.Name = a.Name;
            return View();

        }
        [Permission("Agent")]

        public IActionResult AgentTask()
        {
            var claimdata = HttpContext.GetClaimsData();
            ViewBag.Id = claimdata.Id;
            ViewBag.Address = claimdata.Address;
            return View();


        }

        [Permission("Agent")]
        public IActionResult AgentReceiverTaskJson(AgentTaskModel agent)
        {

            var a = _agentdashservices.GetAgentTask(agent);
            return Json(a);
        }
        [Permission("Agent")]
        public IActionResult AgentDeliveryTaskJson(AgentTaskModel agent)
        {
            var a = _agentdashservices.GetAgentDeliveryTask(agent);
            return Json(a);
        }
        [Permission("Agent")]
        public IActionResult DeliveryStatusAgent1(OrderStatus order)
        {
            var a = _agentdashservices.getOrderStatusByAgent1(order);
            var claimdata = HttpContext.GetClaimsData();
            var cache = _redis.GetDatabase();
            cache.KeyDeleteAsync($"AgentDeliveryTask:{claimdata.Address}");
            return Json(a);
        }
        [Permission("Agent")]
        public IActionResult AgentPastRecords()
        {
            var claimdata = HttpContext.GetClaimsData();
            ViewBag.Id = claimdata.Id;
            return View();
        }
        [Permission("Agent")]
        public IActionResult AgentRecord(AgentTaskModel agent)
        {
            var a = _agentdashservices.GetAgentRecords(agent);
            return Json(a);

        }
        [Permission("Agent")]
        public IActionResult AgentDashboard()
        {
            AgentDashboardGraph();
            var claimData = HttpContext.GetClaimsData();
            if(claimData.Name != null) {
                ViewBag.Name = claimData.Name;
            }
            ViewBag.ChartLabels = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            var TotalOrder = TempData["TotalOrder"];
            var LiveOrder = TempData["LiveOrder"];
            var CompletedOrder = TempData["CompletedOrder"];
            ViewBag.TotalSeries = TotalOrder;
            ViewBag.LiveSeries = LiveOrder;
            ViewBag.PastSeries = CompletedOrder;
            return View();
        }
        public IActionResult AgentDashboardGraph()
        {
            var a = HttpContext.GetClaimsData();
            ViewBag.ID = a.Id;
            var b = _agentdashservices.AgentDashboardGraph(Convert.ToInt32(a.Id));
            TempData["TotalOrder"] = b.TotalOrder;
            TempData["LiveOrder"] = b.LiveOrder;
            TempData["CompletedOrder"] = b.CompletedOrder;
            return Json(b);
        }

    }
}
