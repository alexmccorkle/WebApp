namespace MyShop.Models;

public class Order
{
  public int OrderId { get; set; }
  public String Orderdate { get; set; } = string.Empty;
  public int CustomerId { get; set; }

  //Navigation property
  public Customer Customer { get; set; } = default!; // An order belongs to a customer

  //Navigation property
  public List<OrderItem>? OrderItems { get; set; } // An order can have many order items

  public decimal TotalPrice { get; set; }

}