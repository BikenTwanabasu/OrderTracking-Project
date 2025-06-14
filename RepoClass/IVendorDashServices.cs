using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface IVendorDashServices
    {
        public List<AgentTaskModel> GetVendorKoList(AgentTaskModel vendor);
        public List<AgentTaskModel> GetVendorKoRecord(AgentTaskModel vendor);
        public AgentTaskModel DeleteOrderByVendor(AgentTaskModel model);
        public AgentTaskModel VendorDashboardGraph(int CompanyId);
    }
}
