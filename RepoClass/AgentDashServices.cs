
using CollegeProject.Models;
using CollegeProject.RepoClass;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data.SqlClient;

namespace collegeproject.repoclass
{
    public class agentdashservices: Iagentdashservices
    {
        private IConfiguration _configuration;
        private readonly IConnectionMultiplexer _redisConnection;
        public agentdashservices(IConfiguration configuration,IConnectionMultiplexer redisConnection)
        {
            _configuration = configuration;
            _redisConnection = redisConnection;
        }

        public string Connection()
        {
            var constr = _configuration.GetConnectionString("Dbss");
            return constr;
        }

        public List<AgentTaskModel> GetAgentTask(AgentTaskModel agentM)
        {
            var db = _redisConnection.GetDatabase();
            string Cache = $"AgentDeliveryTask:{agentM.AgentAddress}";

            var CachedData = db.StringGet(Cache);
            if (!CachedData.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<List<AgentTaskModel>>(CachedData.ToString());
            }
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
                    agent.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    agentTasksList.Add(agent);
                }
                db.StringSet(Cache, JsonConvert.SerializeObject(agentTasksList), TimeSpan.FromMinutes(10));
                return agentTasksList;
            }
        }

        public List<AgentTaskModel> GetAgentDeliveryTask(AgentTaskModel agentA)
        {
            var db = _redisConnection.GetDatabase();
            string Cache = $"AgentDeliveryTask:{agentA.AgentAddress}";

            var CachedData = db.StringGet(Cache);
            if (!CachedData.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<List<AgentTaskModel>>(CachedData.ToString());
            }
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
                    agent.DeliveryCharge = rdr["DeliveryCharge"].ToString();
                    agent.TotalAmount = rdr["TotalAmount"].ToString();
                    agent.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    agentDeliveryTasksList.Add(agent);
                }
                db.StringSet(Cache, JsonConvert.SerializeObject(agentDeliveryTasksList), TimeSpan.FromMinutes(10));
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

                while(rdr.Read())
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
        public AgentTaskModel AgentDashboardGraph(int AgentId)
        {
            AgentTaskModel model = new AgentTaskModel();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentDashboardGraph");
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    model.TotalOrder = Convert.ToInt32(rdr["TotalOrder"]);
                }

                if (rdr.NextResult() && rdr.Read())
                {
                    model.LiveOrder = Convert.ToInt32(rdr["LiveOrder"]);
                }

                if (rdr.NextResult() && rdr.Read())
                {
                    model.CompletedOrder = Convert.ToInt32(rdr["CompletedOrder"]);
                }
                return model;


            }
        }
    }
}
