using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class ItemController : Controller
{
  private readonly ItemDbContext _itemDbContext; // This is a private field that holds a reference to the ItemDbContext object

  public ItemController(ItemDbContext itemDbContext)
  {
    _itemDbContext = itemDbContext;
  }

  public IActionResult Table()
  {
    List<Item> items = _itemDbContext.Items.ToList(); // This line retrieves all items from the database and stores them in a list
    var itemsViewModel = new ItemsViewModel(items, "Table");
    return View(itemsViewModel);
  }

  public IActionResult Grid()
  {
    List<Item> items = _itemDbContext.Items.ToList();
    var itemsViewModel = new ItemsViewModel(items, "Grid");
    return View(itemsViewModel);
  }

  public IActionResult Details(int id)
  {
    List<Item> items = _itemDbContext.Items.ToList();
    var item = items.FirstOrDefault(i => i.ItemId == id);
    // FirstOrDefault() is a LINQ method that returns the first element of a sequence that satisfies a condition or a default value if no such element is found.
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }

  // CREATE
  [HttpGet] // GET
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost] // POST
  // Creates a new item in the database and redirects to the Table view to show
  public IActionResult Create(Item item)
  {
    if (ModelState.IsValid) // Did it pass the validation rules?
    {
      _itemDbContext.Items.Add(item);
      _itemDbContext.SaveChanges();
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }



  // UPDATE

  [HttpGet]
  public IActionResult Update(int id)
  {
    var item = _itemDbContext.Items.Find(id); // Find the item with the given id
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }
  [HttpPost]
  public IActionResult Update(Item item)
  {
    if (ModelState.IsValid)
    {
      _itemDbContext.Items.Update(item);
      _itemDbContext.SaveChanges();
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }

  // DELETE
  [HttpGet]
  public IActionResult Delete(int id)
  {
    var item = _itemDbContext.Items.Find(id); // Finds the item
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }

  [HttpPost]
  public IActionResult DeleteConfirmed(int id)
  {
    var item = _itemDbContext.Items.Find(id); // Checks if the item exists
    if (item == null)
    {
      return NotFound();
    }
    _itemDbContext.Items.Remove(item); // Removes item
    _itemDbContext.SaveChanges(); // Saves changes
    return RedirectToAction(nameof(Table)); // Redirects to the Table view
  }


}