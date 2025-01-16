using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface IServices
    {
        public bool RegisterVendor(Vendor vendor);
        public bool RegisterAgent(Agent agent);
        public bool OrderCreations(OrderAndStudentModel orderAndStudentModel);
        public Agent AgentLogIn(Agent agent);
        public Vendor VendorLogIn(Vendor vendor);
        public List<AgentTaskModel> getStatusByOrderID(AgentTaskModel Value1);


    }
}
