using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface Iagentdashservices
    {
        public List<AgentTaskModel> GetAgentTask(AgentTaskModel agent);
        public OrderStatus getOrderStatusByAgent1(OrderStatus orderS);
        public List<AgentTaskModel> GetAgentRecords(AgentTaskModel agentM);
        public List<AgentTaskModel> GetAgentDeliveryTask(AgentTaskModel agentA);
    }
}
