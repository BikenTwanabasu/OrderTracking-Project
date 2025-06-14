using CollegeProject.Models;
using System.Data.SqlClient;

namespace CollegeProject.RepoClass
{
    public class VendorDashServices : IVendorDashServices
    {
        private IConfiguration _configuration;
        public VendorDashServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Connection()
        {
            var constr = _configuration.GetConnectionString("Dbss");
            return constr;
        }

        public List<AgentTaskModel> GetVendorKoList(AgentTaskModel vendor)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            { con.Open();
                List<AgentTaskModel> list = new List<AgentTaskModel>();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "GetVendorDashList");
                cmd.Parameters.AddWithValue("@CompanyId", vendor.CompanyID);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel model = new AgentTaskModel();
                    model.OrderId = rdr["OrderId"].ToString();
                    model.CustomerName = rdr["Cust_Name"].ToString();
                    model.CustomerAddress = rdr["Cust_Address"].ToString();
                    model.CustomerPhone = rdr["Cust_Phone"].ToString();
                    model.ReceiverAgentName = rdr["ReceiverAgentName"].ToString();
                    model.ReceiverAgentPhone = rdr["ReceiverAgentPhone"].ToString();
                    model.DeliveryAgentName = rdr["DeliveryAgentName"].ToString();
                    model.DeliveryAgentPhone = rdr["DeliveryAgentPhone"].ToString();
                    model.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    list.Add(model);
                }
                return list;
            }

        }
        public List<AgentTaskModel> GetVendorKoRecord(AgentTaskModel vendor)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                List<AgentTaskModel> list = new List<AgentTaskModel>();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "GetVendorDashHistory");
                cmd.Parameters.AddWithValue("@CompanyId", vendor.CompanyID);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AgentTaskModel model = new AgentTaskModel();
                    model.OrderId = rdr["OrderId"].ToString();
                    model.CustomerName = rdr["Cust_Name"].ToString();
                    model.CustomerAddress = rdr["Cust_Address"].ToString();
                    model.CustomerPhone = rdr["Cust_Phone"].ToString();
                    model.ReceiverAgentName = rdr["ReceiverAgentName"].ToString();
                    model.ReceiverAgentPhone = rdr["ReceiverAgentPhone"].ToString();
                    model.DeliveryAgentName = rdr["DeliveryAgentName"].ToString();
                    model.DeliveryAgentPhone = rdr["DeliveryAgentPhone"].ToString();
                    model.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    list.Add(model);
                }
                return list;
            }

        }
        public AgentTaskModel DeleteOrderByVendor(AgentTaskModel model)
        {
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "DeleteOrderByVendor");
                cmd.Parameters.AddWithValue("@OrderId", model.OrderId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    model.ResponseCode = rdr["ResponseCode"].ToString();
                    model.ResponseMessage = rdr["ResponseMessage"].ToString();
                }
                return model;


            }
        }
        public AgentTaskModel VendorDashboardGraph(int CompanyId)
        {
            AgentTaskModel model = new AgentTaskModel();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "VendorDashboardGraph");
                cmd.Parameters.AddWithValue("@CompanyId",CompanyId);
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
