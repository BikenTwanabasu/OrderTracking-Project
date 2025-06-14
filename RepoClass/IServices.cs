using CollegeProject.Models;

namespace CollegeProject.RepoClass
{
    public interface IServices
    {
        public Vendor RegisterVendor(Vendor vendor);
        public Agent RegisterAgent(Agent agent);
        public bool OrderCreations(OrderAndStudentModel orderAndStudentModel);
        public Agent AgentLogIn(Agent agent);
        public Vendor VendorLogIn(Vendor vendor);
        public List<AgentTaskModel> getStatusByOrderID(AgentTaskModel Value1);
        public Admin RegisterAdmin(Admin admin);


    }
}
