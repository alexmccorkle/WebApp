using MyShop.Models;

namespace MyShop.DAL;

public interface IItemRepository
{
  Task<IEnumerable<Item>?> GetAll();
  Task<Item?> GetItemById(int id);
  // <Item?> means that it can return null, i.e. it might not contain an item
  Task<bool> Create(Item item);
  Task<bool> Update(Item item);
  Task<bool> Delete(int id);
}