using CollegeProject.Models;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography.Pkcs;

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
                    modelA.CustomerAddress= rdr["DeliveryAddress"].ToString();
                    modelA.CustomerPhone= rdr["Cust_Phone"].ToString();
                    modelA.CreatedDate= rdr["OrderPlacementDate"].ToString();
                    modelA.ReceiverAgentId= rdr["ReceiverAgentID"].ToString();
                    modelA.ReceiverAgentName= rdr["ReceiverAgentName"].ToString();
                    modelA.ReceiverAgentPhone= rdr["ReceiverAgentPhone"].ToString();
                    modelA.DeliveredDate= rdr["DeliveryDate"].ToString();
                    modelA.DeliveryAgentId= rdr["DeliveryAgentID"].ToString();
                    modelA.DeliveryAgentName= rdr["DeliveryAgentName"].ToString();
                    modelA.DeliveryAgentPhone= rdr["DeliveryAgentPhone"].ToString();
                    modelA.DeliveryCharge = rdr["DeliveryCharge"].ToString();
                    modelA.TotalAmount = rdr["TotalAmount"].ToString();
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
                    modelA.DeliveryCharge = rdr["DeliveryCharge"].ToString();
                    modelA.TotalAmount = rdr["TotalAmount"].ToString();
                    modelA.DeliveryStatus = rdr["DeliveryStatus"].ToString();

                    list.Add(modelA);

                }
                return list;
            }
        }

        public AgentTaskModel deleteByAdmin(AgentTaskModel model)
        {
            using(SqlConnection con =new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "DeleteOrderByAdmin");
                cmd.Parameters.AddWithValue("@OrderId", model.OrderId);

                SqlDataReader rdr= cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.ResponseCode = rdr["ResponseCode"].ToString();
                    model.ResponseMessage = rdr["ResponseMessage"].ToString();

                }
                return model;
            }
        }

        public AgentTaskModel Getorder(AgentTaskModel model)
        {
              
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "GetOrderById");
                cmd.Parameters.AddWithValue("@OrderId",model.OrderId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    model.CustomerAddress = rdr["DeliveryAddress"].ToString();
                    model.DeliveredDate = Convert.ToDateTime( rdr["DeliveryDate"]).ToString("MM/dd/yyyy");
                    model.Amount = rdr["Amount"].ToString();
                    model.DeliveryCharge = rdr["DeliveryCharge"].ToString();
                    model.CreatedDate = Convert.ToDateTime( rdr["CreatedDate"]).ToString("MM/dd/yyyy");
                    model.DeliveryStatus = rdr["DeliveryStatus"].ToString();
                    model.CustomerPhone = rdr["Cust_Phone"].ToString();
                    model.VendorPhone = rdr["CompanyPhone"].ToString() ;

                }
                return model;
            } 
        }

        public bool updateOrderDataByAdmin(AgentTaskModel model)
        {
            var i = 0;
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "UpdateByAdmin");
                cmd.Parameters.AddWithValue("@OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("@DeliveryAddress",model.CustomerAddress);
                cmd.Parameters.AddWithValue("@DeliveryDate",model.DeliveredDate);
                cmd.Parameters.AddWithValue("@Amount",model.Amount);
                cmd.Parameters.AddWithValue("@DeliveryCharge",model.DeliveryCharge);
                cmd.Parameters.AddWithValue("@CreatedDate",model.CreatedDate);
                cmd.Parameters.AddWithValue("@DeliveryStatus",model.DeliveryStatus);
                cmd.Parameters.AddWithValue("@CompanyPhone",model.VendorPhone);
                cmd.Parameters.AddWithValue("@Cust_Phone",model.CustomerPhone);

                i=cmd.ExecuteNonQuery();

            }
            return i > 0;

        }
        public List<AgentTaskModel> VendorRegistrationRequest()
        {
            List<AgentTaskModel> list = new List<AgentTaskModel>();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "VendorRegistrationRequestList");
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    AgentTaskModel agentTaskModel = new AgentTaskModel();
                    agentTaskModel.CompanyID = rdr["TempCompanyId"].ToString();
                    agentTaskModel.VendorName = rdr["CompanyName"].ToString();
                    agentTaskModel.VendorAddress = rdr["CompanyAddress"].ToString();
                    agentTaskModel.VendorEmail= rdr["CompanyEmail"].ToString();
                    agentTaskModel.VendorPhone = rdr["CompanyPhone"].ToString();
                    agentTaskModel.RegisterStatus = rdr["RegisterStatus"].ToString();

                    list.Add(agentTaskModel);
                }
                return list;
            }
        }
        public AgentTaskModel AcceptVendorRegistrationRequest(int TempCompanyId)
        {
            AgentTaskModel model = new AgentTaskModel();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AcceptVendorRegistrationRequest");
                cmd.Parameters.AddWithValue("@TempCompanyId", TempCompanyId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.ResponseCode = rdr["ResponseCode"].ToString();
                    model.ResponseMessage = rdr["ResponseMessage"].ToString();

                }

            }
            return model;
        }
        public List<AgentTaskModel> AgentRegistrationRequest()
        {
            List<AgentTaskModel> list = new List<AgentTaskModel>();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AgentRegistrationRequestList");
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    AgentTaskModel agentTaskModel = new AgentTaskModel();
                    agentTaskModel.AgentId = rdr["TempAgentId"].ToString();
                    agentTaskModel.AgentName = rdr["AgentName"].ToString();
                    agentTaskModel.AgentAddress = rdr["AgentAddress"].ToString();
                    agentTaskModel.AgentEmail = rdr["AgentEmail"].ToString();
                    agentTaskModel.AgentPhone = rdr["AgentPhone"].ToString();
                    agentTaskModel.RegisterStatus = rdr["RegisterStatus"].ToString();

                    list.Add(agentTaskModel);
                }
                return list;
            }
        }
        public AgentTaskModel AcceptAgentRegistrationRequest(int TempAgentId)
        {
            AgentTaskModel model = new AgentTaskModel();
            using (SqlConnection con = new SqlConnection(Connection()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "AcceptAgentRegistrationRequest");
                cmd.Parameters.AddWithValue("@TempAgentId", TempAgentId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.ResponseCode = rdr["ResponseCode"].ToString();
                    model.ResponseMessage = rdr["ResponseMessage"].ToString();

                }

            }
            return model;
        }
    }
}
