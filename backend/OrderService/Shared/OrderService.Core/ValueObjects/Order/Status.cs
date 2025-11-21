namespace OrderService.Core.ValueObjects.Order;

public enum Status
{
    Created = 0,      
    Pending = 1,      
    Confirmed = 2,   
    Processing = 3,   
    Shipped = 4,      
    Delivered = 5,    
    Cancelled = 6,   
    Refunded = 7,    
    OnHold = 8           
}