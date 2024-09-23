using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Controllers;

public class CustomerController : Controller
{
  private readonly ItemDbContext _itemDbContext;
  // This is a private field that holds a reference to the ItemDbContext object

  public CustomerController(ItemDbContext itemDbContext)
  {
    _itemDbContext = itemDbContext;
  }

  public async Task<IActionResult> Table()
  // Retrieves all customers from DB and stores in list
  {
    List<Customer> customers = await _itemDbContext.Customers.ToListAsync();
    // Await = Pauses execution until the asynchronous operation completes
    return View(customers);
  }
}