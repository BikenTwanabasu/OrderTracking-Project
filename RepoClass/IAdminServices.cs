using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface IAdminServices
    {
        public List<AgentTaskModel> getAdminPresentList(AgentTaskModel model);
        public AgentTaskModel UpdateDeliveryStatusByAdmin(AgentTaskModel model);
        public List<AgentTaskModel> getAdminHistoryList(AgentTaskModel model);
        public AgentTaskModel deleteByAdmin(AgentTaskModel model);
        public AgentTaskModel Getorder(AgentTaskModel model);
        public bool updateOrderDataByAdmin(AgentTaskModel model);
        public List<AgentTaskModel> VendorRegistrationRequest();
        public AgentTaskModel AcceptVendorRegistrationRequest(int TempCompanyId);
        public List<AgentTaskModel> AgentRegistrationRequest();
        public AgentTaskModel AcceptAgentRegistrationRequest(int TempAgentId);
        public List<VendorOrderChartData> AdminDashboardGraph();
    }
}
