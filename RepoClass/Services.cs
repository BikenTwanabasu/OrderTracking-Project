using CollegeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Pipelines.Sockets.Unofficial.Arenas;
using System.Data.SqlClient;
using System.Security.Claims;

namespace CollegeProject.RepoClass
{
    public class Services : IServices
    {
        private IConfiguration _configuration;
        private readonly IPasswordService _passwordService;

        public Services(IConfiguration configuration, IPasswordService passwordService)
        {
            _configuration = configuration;
            _passwordService = passwordService;
        }

        public string ConnectionString()
        {
            string Constr = _configuration.GetConnectionString("Dbss");
            return Constr;
        }
        public Vendor RegisterVendor(Vendor vendor)
        {

            if (vendor.CompanyName == null && vendor.CompanyEmail == null) { return vendor; }

            string hashedPassword = _passwordService.HashPassword(vendor.Password);


            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", vendor.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyAddress", vendor.CompanyAddress);
                cmd.Parameters.AddWithValue("@CompanyPhone", vendor.CompanyPhone);
                cmd.Parameters.AddWithValue("@CompanyEmail", vendor.CompanyEmail);
                cmd.Parameters.AddWithValue("@CompanyPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@flag", "VendorRegistration");

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    vendor.ResponseCode = (int)rdr["ResponseCode"];
                    vendor.ResponseMessage = rdr["ResponseMessage"].ToString();

                }
                return vendor;

            }

        }

        public Agent RegisterAgent(Agent agent)
        {

            if (agent.AgentEmail == null && agent.AgentPhone == null && agent.AgentAddress == null && agent.AgentPassword == null)
            { return agent; }
            string hashedPassword = _passwordService.HashPassword(agent.AgentPassword);

            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgentName", agent.AgentName);
                cmd.Parameters.AddWithValue("@AgentEmail", agent.AgentEmail);
                cmd.Parameters.AddWithValue("@AgentPhone", agent.AgentPhone);
                cmd.Parameters.AddWithValue("@AgentAddress", agent.AgentAddress);
                cmd.Parameters.AddWithValue("@AgentPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@flag", "AgentRegistration");

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    agent.ResponseCode = (int)rdr["ResponseCode"];
                    agent.ResponseMessage = rdr["ResponseMessage"].ToString();
                }
                return agent;
            }

        }
        public bool OrderCreations(OrderAndStudentModel orderAndStudentModel)
        {
            int i = 0;
            if (orderAndStudentModel == null)
            {
                return false;
            }
            if (orderAndStudentModel.customer == null || orderAndStudentModel.order == null)
            {
                return false;
            }


            if (orderAndStudentModel.customer.CustomerName == null || orderAndStudentModel.customer.OrderReceiveDate == null || orderAndStudentModel.customer.Email == null || orderAndStudentModel.customer.CustomerAddress == null
                || orderAndStudentModel.order.DeliveryAddress == null || orderAndStudentModel.order.DeliveryDate == null || orderAndStudentModel.order.PaymentStatus == null)
            { return false; }


            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cust_Name", orderAndStudentModel.customer.CustomerName);
                cmd.Parameters.AddWithValue("@Cust_Address", orderAndStudentModel.customer.CustomerAddress);
                cmd.Parameters.AddWithValue("@Cust_Phone", orderAndStudentModel.customer.Phone);
                cmd.Parameters.AddWithValue("@Cust_Email", orderAndStudentModel.customer.Email);
                cmd.Parameters.AddWithValue("@CreatedDate", orderAndStudentModel.customer.OrderReceiveDate);
                cmd.Parameters.AddWithValue("@Amount", orderAndStudentModel.order.Amount);
                cmd.Parameters.AddWithValue("@PaymentStatus", orderAndStudentModel.order.PaymentStatus);
                cmd.Parameters.AddWithValue("@DeliveryAddress", orderAndStudentModel.order.DeliveryAddress);
                cmd.Parameters.AddWithValue("@DeliveryDate", orderAndStudentModel.order.DeliveryDate);
                cmd.Parameters.AddWithValue("@CompanyId", orderAndStudentModel.order.CompanyId);

                cmd.Parameters.AddWithValue("@flag", "OrderCreation");

                i = cmd.ExecuteNonQuery();
                return i > 0;
            }

        }
        public Agent AgentLogIn(Agent agent)
        {
            if (agent.AgentEmail == null)
            {
                return agent;
            }
            string hashedPassword = _passwordService.HashPassword(agent.AgentPassword);
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgentEmail", agent.AgentEmail);
                cmd.Parameters.AddWithValue("@AgentPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@flag", "AgentLogIn");
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    agent.ResponseCode = (int)rdr["ResponseCode"];
                    agent.ResponseMessage = rdr["ResponseMessage"].ToString();
                    agent.AgentId = rdr["AgentId"].ToString();
                    agent.AgentName = rdr["AgentName"].ToString();
                    agent.AgentAddress = rdr["AgentAddress"].ToString();
                }
                return agent;

            }

        }
        public Vendor VendorLogIn(Vendor vendor)
        {
            if (vendor.CompanyEmail == null)
            {
                return vendor;
            }
            string UIPassword = vendor.Password;
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyEmail", vendor.CompanyEmail);
                cmd.Parameters.AddWithValue("@flag", "VendorLogIn");
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    vendor.ResponseCode = (int)rdr["ResponseCode"];
                    vendor.ResponseMessage = rdr["ResponseMessage"].ToString();
                    vendor.Password = rdr["CompanyPassword"].ToString();
                    vendor.CompanyId = rdr["CompanyId"].ToString();
                    vendor.CompanyName = rdr["CompanyName"].ToString();
                    vendor.CompanyAddress = rdr["CompanyAddress"].ToString();

                }

                bool _passwordIsCorrect = _passwordService.VerifyPassword(vendor.Password, UIPassword);
                if(_passwordIsCorrect)
                {
                    vendor.ResponseCode = 1;
                    vendor.ResponseMessage = "Login Successful";
                }
                return vendor;

            }
        }

        public List<AgentTaskModel> getStatusByOrderID(AgentTaskModel Value1)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                List<AgentTaskModel> list = new List<AgentTaskModel>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insertDatas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "OrderStatusSearch");
                cmd.Parameters.AddWithValue("@OrderId", Value1.OrderId);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    AgentTaskModel value = new AgentTaskModel();
                    value.ResponseCode = rdr["ResponseCode"].ToString();
                    if (value.ResponseCode == "101")
                    {
                        value.DeliveryStatus = rdr["DeliveryStatus"].ToString();
                        value.AgentName = rdr["AgentName"].ToString();
                        value.AgentPhone = rdr["AgentPhone"].ToString();
                    }
                    else
                    {

                        value.ResponseMessage = rdr["ResponseMessage"].ToString();
                    }

                    list.Add(value);
                }
                return list;
            }
        }


    }
}
