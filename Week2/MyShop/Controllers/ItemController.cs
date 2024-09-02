using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;


namespace MyShop.Controllers;

// Controller is a class that handles HTTP requests
// In C#, controller format is PascalCase
public class ItemController : Controller
{
  public IActionResult Table()
  {
    // Initialize a list of items
    var items = new List<Item>();
    var item1 = new Item();

    // One way to set the properties of the item
    item1.ItemId = 1;
    item1.Name = "Pizza";
    item1.Price = 60;

    // Another way to set the properties of the item, THE PREFERRABLE WAY
    var item2 = new Item
    {
      ItemId = 2,
      Name = "Fried Chicken Leg",
      Price = 15
    };

    // Add items to the list
    items.Add(item1);
    items.Add(item2);

    ViewBag.CurrentViewName = "List of Shop Items";
    return View(items);
  }
}

// Actions: 
// Method in a controller that handles HTTP requests. Each action method typically responds to user interaction, such as viewing a page, submitting a form, or clicking a button.
// In C#, action format is PascalCase
// Return types:
// Usually returns IActionResult, which is a base class for all action results in ASP.NET Core
// These can be a View, a Redirect, a JSON object, etc.

// In this example: 
// Table() is an action method that creates a list of items in a mock fashion
// And then sets a value in ViewBag to pass additional data to the View
// Finally, it returns the View with the list of items



