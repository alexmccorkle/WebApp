using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.DAL;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class ItemController : Controller
{
  private readonly IItemRepository _itemRepository; // This is a private field that holds a reference to the ItemDbContext object
  private readonly ILogger<ItemController> _logger;

  public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
  {
    _itemRepository = itemRepository;
    _logger = logger;
  }

  public async Task<IActionResult> Table()
  {
    var items = await _itemRepository.GetAll();
    if (items == null)
    {
      _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
      return NotFound("Item list not found");
    }
    var itemsViewModel = new ItemsViewModel(items, "Table");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Grid()
  {
    var items = await _itemRepository.GetAll();
    if (items == null)
    {
      _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
      return NotFound("Item list not found");
    }
    var itemsViewModel = new ItemsViewModel(items, "Grid");
    return View(itemsViewModel);
  }

  public async Task<IActionResult> Details(int id)
  {
    var item = await _itemRepository.GetItemById(id);
    // FirstOrDefault() = LINQ method that returns first element of sequence that satisfies condition or default value if no element found.
    if (item == null)
    {
      _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
      return NotFound("Item not found for the ItemId");
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
      bool returnOK = await _itemRepository.Create(item);
      if (returnOK)
        return RedirectToAction(nameof(Table));
    }
    _logger.LogWarning("[ItemController] Item creation failed {@item}", item);
    return View(item);
  }


  // UPDATE

  [HttpGet]
  public async Task<IActionResult> Update(int id)
  {
    var item = await _itemRepository.GetItemById(id);// Find the item with the given id
    if (item == null)
    {
      _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
      return BadRequest("Item not found for the ItemId");
    }
    return View(item);
  }

  [HttpPost]
  public async Task<IActionResult> Update(Item item)
  {
    if (ModelState.IsValid)
    {
      bool returnOK = await _itemRepository.Update(item);
      if (returnOK)
        return RedirectToAction(nameof(Table));
    }
    _logger.LogWarning("[ItemController] Item update failed {@item}", item);
    return View(item);
  }

  // DELETE
  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    var item = await _itemRepository.GetItemById(id);// Finds the item
    if (item == null)
    {
      _logger.LogError("[ItemController] Item not found for the item {itemId:0000}", id);
      return BadRequest("Item not found for that ItemId");
    }
    return View(item);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    bool returnOK = await _itemRepository.Delete(id); // Saves changes
    if (!returnOK)
    {
      _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
      return BadRequest("Item deletion failed!");
    }
    return RedirectToAction(nameof(Table)); // Redirects to the Table view
  }


}