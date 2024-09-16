using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

  public async Task<IActionResult> Table()
  {
    List<Item> items = await _itemDbContext.Items.ToListAsync(); // This line retrieves all items from the database and stores them in a list
    var itemsViewModel = new ItemsViewModel(items, "Table");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Grid()
  {
    List<Item> items = await _itemDbContext.Items.ToListAsync();
    var itemsViewModel = new ItemsViewModel(items, "Grid");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Details(int id)
  {
    var item = await _itemDbContext.Items.FirstOrDefaultAsync(i => i.ItemId == id);
    // FirstOrDefault() = LINQ method that returns first element of sequence that satisfies condition or default value if no element found.
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
  public async Task<IActionResult> Create(Item item)
  {
    if (ModelState.IsValid) // Did it pass the validation rules?
    {
      _itemDbContext.Items.Add(item);
      await _itemDbContext.SaveChangesAsync();
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }


  // UPDATE

  [HttpGet]
  public async Task<IActionResult> Update(int id)
  {
    var item = await _itemDbContext.Items.FindAsync(id); // Find the item with the given id
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }
  [HttpPost]
  public async Task<IActionResult> Update(Item item)
  {
    if (ModelState.IsValid)
    {
      _itemDbContext.Items.Update(item);
      await _itemDbContext.SaveChangesAsync();
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }

  // DELETE
  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    var item = await _itemDbContext.Items.FindAsync(id); // Finds the item
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var item = await _itemDbContext.Items.FindAsync(id); // Checks if the item exists
    if (item == null)
    {
      return NotFound();
    }
    _itemDbContext.Items.Remove(item); // Removes item
    await _itemDbContext.SaveChangesAsync(); // Saves changes
    return RedirectToAction(nameof(Table)); // Redirects to the Table view
  }


}