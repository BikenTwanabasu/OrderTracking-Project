namespace CollegeProject.Models
{
    public class Customer
    {                
        public string? CustomerId { get; set; }  
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? OrderReceiveDate { get; set; }
    } 
    
    public class Vendor
    {
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyEmail { get; set; }

        public string? Password { get; set; }
        public int? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }
                     
    public class Order
    {                
        public string? OrderId { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Amount { get; set; }
        public string? PaymentStatus { get; set; }
                     
        public string? DeliveryDate { get; set; }    
        public string? OrderReceiveDate { get;set; }
        public string? CustomerId { get; set; }
        public string? CompanyId { get; set; }
    }

    public class Agent
    {
        public string? AgentId { get; set; }
        public string? AgentName { get; set; }
        public string? AgentPhone { get; set; }
        public string? AgentEmail { get; set; }
        public string? AgentAddress { get; set; }

        public string? AgentPassword { get; set; }

        public int? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }

    public class Delivery
    {
        public string? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? AgentId { get; set; }
        public string? DeliveryStatus { get; set; } 
        public string? DeliveryCharge { get; set; }
        public string? DeliveryDate { get; set; }
        public string? OrderReceiveDate { get; set;}
    }

    public class OrderAndStudentModel
    {
        public Customer? customer { get; set; }
        public Order? order { get; set; }
    }
}
