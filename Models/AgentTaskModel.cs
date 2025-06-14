namespace CollegeProject.Models
{
    public class AgentTaskModel
    {
        public string? OrderId { get; set; }
        public string? VendorName { get; set; }
        public string? VendorAddress { get; set; }
        public string? VendorPhone { get; set; }   
        public string? CreatedDate { get; set; } 
        public string? DeliveryStatus { get; set; } 
        public string? AgentId { get; set; }
        public string? DeliveredDate { get; set; }  
        public string? CompanyID { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; } 
        public string? CustomerPhone { get; set; }  
        public string? DeliveryDate { get; set; }
        public string? AgentName { get; set; }
        public string? AgentPhone { get; set; }
        public  string? AgentAddress { get; set; }
        public string? ReceiverAgentId { get; set; }
        public string? ReceiverAgentName { get; set; }
        public string? ReceiverAgentPhone { get; set; }
        public string? DeliveryAgentId { get; set; }
        public string? DeliveryAgentName { get; set; }
        public string? DeliveryAgentPhone { get; set; } 
        public string? ResponseMessage { get; set; }
        public string? ResponseCode { get; set; }

        public string? DeliveryCharge { get; set; }
        public string? TotalAmount { get; set; }
        public string? Amount { get; set; }
        public string? VendorEmail { get; set; }
        public string? RegisterStatus { get; set; }
        public string? AgentEmail { get; set; }
        public int TotalOrder { get; set; } 
        public int LiveOrder { get; set; }  
        public int CompletedOrder { get; set; }
    }
}
