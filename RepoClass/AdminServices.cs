using CollegeProject.Models;
using System.Data.SqlClient;

namespace CollegeProject.RepoClass
{
    public class AdminServices : IAdminServices
    {
        private IConfiguration _configuration;
        public AdminServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Connection()
        {
            var constr = _configuration.GetConnectionString("Dbss");
            return constr;
        }
        public List<AgentTaskModel> getAdminPresentList(AgentTaskModel model)
        {
            using(SqlConnection con = new SqlConnection(Connection())) {

                List<AgentTaskModel> list = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType =System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AllOngoingAdminOrderView");
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel modelA=new AgentTaskModel();
                    modelA.OrderId = rdr["OrderId"].ToString();
                    modelA.CompanyID = rdr["CompanyID"].ToString();
                    modelA.VendorName= rdr["CompanyName"].ToString();
                    modelA.VendorPhone= rdr["CompanyPhone"].ToString();
                    modelA.CustomerId= rdr["Cust_ID"].ToString();
                    modelA.CustomerName= rdr["Cust_Name"].ToString();
                    modelA.CustomerAddress= rdr["Cust_Address"].ToString();
                    modelA.CustomerPhone= rdr["Cust_Phone"].ToString();
                    modelA.CreatedDate= rdr["OrderPlacementDate"].ToString();
                    modelA.ReceiverAgentId= rdr["ReceiverAgentID"].ToString();
                    modelA.ReceiverAgentName= rdr["ReceiverAgentName"].ToString();
                    modelA.ReceiverAgentPhone= rdr["ReceiverAgentPhone"].ToString();
                    modelA.DeliveredDate= rdr["DeliveryDate"].ToString();
                    modelA.DeliveryAgentId= rdr["DeliveryAgentID"].ToString();
                    modelA.DeliveryAgentName= rdr["DeliveryAgentName"].ToString();
                    modelA.DeliveryAgentPhone= rdr["DeliveryAgentPhone"].ToString();
                    modelA.DeliveryStatus= rdr["DeliveryStatus"].ToString();

                    list.Add(modelA);
                    
                }
                return list;
            }
        }

        public AgentTaskModel UpdateDeliveryStatusByAdmin(AgentTaskModel model)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "UpdateStatusByAdmin");
                cmd.Parameters.AddWithValue("@OrderId",model.OrderId );
                SqlDataReader rdr =cmd.ExecuteReader();

                while (rdr.Read())
                {
                    model.DeliveryStatus = rdr["DeliveryStatus"].ToString();
                }
                return model;

            }
        }

            public List<AgentTaskModel> getAdminHistoryList(AgentTaskModel model)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {

                List<AgentTaskModel> list = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AllPastAdminOrderView");
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel modelA = new AgentTaskModel();
                    modelA.OrderId = rdr["OrderId"].ToString();
                    modelA.CompanyID = rdr["CompanyID"].ToString();
                    modelA.VendorName = rdr["CompanyName"].ToString();
                    modelA.VendorPhone = rdr["CompanyPhone"].ToString();
                    modelA.CustomerId = rdr["Cust_ID"].ToString();
                    modelA.CustomerName = rdr["Cust_Name"].ToString();
                    modelA.CustomerAddress = rdr["Cust_Address"].ToString();
                    modelA.CustomerPhone = rdr["Cust_Phone"].ToString();
                    modelA.CreatedDate = rdr["OrderPlacementDate"].ToString();
                    modelA.ReceiverAgentId = rdr["ReceiverAgentID"].ToString();
                    modelA.ReceiverAgentName = rdr["ReceiverAgentName"].ToString();
                    modelA.ReceiverAgentPhone = rdr["ReceiverAgentPhone"].ToString();
                    modelA.DeliveredDate = rdr["DeliveryDate"].ToString();
                    modelA.DeliveryAgentId = rdr["DeliveryAgentID"].ToString();
                    modelA.DeliveryAgentName = rdr["DeliveryAgentName"].ToString();
                    modelA.DeliveryAgentPhone = rdr["DeliveryAgentPhone"].ToString();
                    modelA.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    list.Add(modelA);

                }
                return list;
            }
        }
    }
}
