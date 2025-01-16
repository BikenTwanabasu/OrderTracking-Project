
using CollegeProject.Models;
using CollegeProject.RepoClass;
using System.Data.SqlClient;

namespace collegeproject.repoclass
{
    public class agentdashservices: Iagentdashservices
    {
        private IConfiguration _configuration;
        public agentdashservices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Connection()
        {
            var constr = _configuration.GetConnectionString("Dbss");
            return constr;
        }

        public List<AgentTaskModel> GetAgentTask(AgentTaskModel agentM)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {   List<AgentTaskModel> agentTasksList = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType=System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentTaskPickupList");
                cmd.Parameters.AddWithValue("@AgentId", agentM.AgentId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel agent = new AgentTaskModel();
                    agent.OrderId = rdr["OrderId"].ToString();
                    agent.VendorName = rdr["CompanyName"].ToString() ;
                    agent.VendorAddress = rdr["CompanyAddress"].ToString();
                    agent.VendorPhone = rdr["CompanyPhone"].ToString();
                    agent.CreatedDate = Convert.ToDateTime( rdr["CreatedDate"]).ToString("yyyy/MM/dd");

                    agentTasksList.Add(agent);
                }
                return agentTasksList;
            }
        }

        public List<AgentTaskModel> GetAgentDeliveryTask(AgentTaskModel agentA)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                List<AgentTaskModel> agentDeliveryTasksList = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentTaskDeliverList");
                cmd.Parameters.AddWithValue("@AgentId", agentA.AgentId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel agent = new AgentTaskModel();
                    agent.OrderId = rdr["OrderId"].ToString();
                    agent.VendorName = rdr["CompanyName"].ToString();
                    agent.CustomerName = rdr["Cust_Name"].ToString() ;
                    agent.CustomerAddress = rdr["Cust_Address"].ToString();
                    agent.CustomerPhone = rdr["Cust_Phone"].ToString();
                    agent.DeliveredDate = Convert.ToDateTime(rdr["DeliveryDate"]).ToString("yyyy/MM/dd");
                    agent.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    agentDeliveryTasksList.Add(agent);
                }
                return agentDeliveryTasksList;
            }
        }

        public OrderStatus getOrderStatusByAgent1(OrderStatus orderS)
        {
            using(SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", orderS.OrderId);
                cmd.Parameters.AddWithValue("@flag", "DeliveryStatusAgent1");
                SqlDataReader rdr =cmd.ExecuteReader();

                while (rdr.Read())
                {
                    orderS.DeliveryStatus = rdr["DeliveryStatus"].ToString();
                }
                return orderS;
            }
        }
        public List<AgentTaskModel> GetAgentRecords(AgentTaskModel agentM)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                List<AgentTaskModel> agentTasksList = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentPastRecords");
                cmd.Parameters.AddWithValue("@AgentId", agentM.AgentId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel agent = new AgentTaskModel();
                    agent.OrderId = rdr["OrderId"].ToString();
                    agent.VendorName = rdr["CompanyName"].ToString();
                    agent.VendorAddress = rdr["CompanyAddress"].ToString();
                    agent.VendorPhone = rdr["CompanyPhone"].ToString();
                    agent.DeliveredDate =Convert.ToDateTime( rdr["DeliveryDate"]).ToString("yyyy/MM/dd");

                    agentTasksList.Add(agent);
                }
                return agentTasksList;
            }
        }

        public List<AgentTaskModel> GetDeliveryTask(AgentTaskModel agent1)
        {
            using(SqlConnection con = new SqlConnection(Connection()))
            {
                List<AgentTaskModel> DeliveryList = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType=System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentTaskDeliverList");
                cmd.Parameters.AddWithValue("@Agent", agent1.AgentId);
                SqlDataReader rdr =cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel agent = new AgentTaskModel();
                    agent.OrderId = rdr["OrderId"].ToString();
                    agent.CustomerName = rdr["Cust_Name"].ToString();
                    agent.CustomerAddress= rdr["Cust_Address"].ToString();
                    agent.CustomerPhone = rdr["Cust_Phone"].ToString();
                    agent.DeliveryDate = Convert.ToDateTime( rdr["DeliveryDate"]).ToString("yyyy/MM/dd");

                    DeliveryList.Add(agent) ;
                }
                return DeliveryList;
            }
        }
    }
}
