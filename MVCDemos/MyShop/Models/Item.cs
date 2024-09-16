// System provides basic types and classes for the C# programming language
using System;

// Namespace is a way to organize code in C#
namespace MyShop.Models
{
  // Class is a blueprint for creating objects same as in Java
  public class Item
  {
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    // Navigation property
    public List<OrderItem>? OrderItems { get; set; }
  }
}