using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.DAL;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class ItemController : Controller
{
  private readonly IItemRepository _itemRepository; // This is a private field that holds a reference to the ItemDbContext object

  public ItemController(IItemRepository itemRepository)
  {
    _itemRepository = itemRepository;
  }

  public async Task<IActionResult> Table()
  {
    var items = await _itemRepository.GetAll();
    var itemsViewModel = new ItemsViewModel(items, "Table");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Grid()
  {
    var items = await _itemRepository.GetAll();
    var itemsViewModel = new ItemsViewModel(items, "Grid");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Details(int id)
  {
    var item = await _itemRepository.GetItemById(id);
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
      await _itemRepository.Create(item);
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }


  // UPDATE

  [HttpGet]
  public async Task<IActionResult> Update(int id)
  {
    var item = await _itemRepository.GetItemById(id);// Find the item with the given id
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
      await _itemRepository.Update(item);
      return RedirectToAction(nameof(Table));
    }
    return View(item);
  }

  // DELETE
  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    var item = await _itemRepository.GetItemById(id);// Finds the item
    if (item == null)
    {
      return NotFound();
    }
    return View(item);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    await _itemRepository.Delete(id); // Saves changes
    return RedirectToAction(nameof(Table)); // Redirects to the Table view
  }


}