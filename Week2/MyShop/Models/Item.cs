// System provides basic types and classes for the C# programming language
using System;

// Namespace is a way to organize code in C#
namespace MyShop.Models
{
  // Class is a blueprint for creating objects same as in Java
  public class Item
  {

    // Properties
    // In C#, property format is PascalCase
    public int ItemId { get; set; }

    // string.Empty is a way to initialize a string to empty string
    // Defaults to empty string to avoid null reference exceptions
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // ? means that the property can be null
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
  }
}