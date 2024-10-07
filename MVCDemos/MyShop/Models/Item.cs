// System provides basic types and classes for the C# programming language
using System;
using System.ComponentModel.DataAnnotations;

// Namespace is a way to organize code in C#
namespace MyShop.Models
{
  // Class is a blueprint for creating objects same as in Java
  public class Item
  {
    public int ItemId { get; set; }

    [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
    [Display(Name = "Item name")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0")]
    public decimal Price { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    // Navigation property
    public virtual List<OrderItem>? OrderItems { get; set; }
  }
}