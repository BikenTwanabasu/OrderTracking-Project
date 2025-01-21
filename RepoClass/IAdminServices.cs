using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface IAdminServices
    {
        public List<AgentTaskModel> getAdminPresentList(AgentTaskModel model);
        public AgentTaskModel UpdateDeliveryStatusByAdmin(AgentTaskModel model);
        public List<AgentTaskModel> getAdminHistoryList(AgentTaskModel model);
    }
}
