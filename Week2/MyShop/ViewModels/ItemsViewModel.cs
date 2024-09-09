using MyShop.Models;

namespace MyShop.ViewModels
{
  public class ItemsViewModel
  {
    public IEnumerable<Item> Items;
    public String? CurrentViewName;

    public ItemsViewModel(IEnumerable<Item> items, String? currentViewName)
    // Constructor. The '?' after the type means that the variable can be null.
    {
      Items = items;
      CurrentViewName = currentViewName;
    }
  }
}