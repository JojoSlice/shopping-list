using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items;
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        //_items = GenerateDemoItems();
        _nextIndex = 4; // We have 4 demo items initialized
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        return _items[.._nextIndex];
    }

    public ShoppingItem? GetById(string id)
    {
        // TODO: Students - Find and return the item with the matching id
        return null;
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {
        var newItem = new ShoppingItem
        {
            Name = name,
            Quantity = quantity,
            Notes = notes,
        };
        _items.Prepend(newItem);
        _nextIndex++;

        return newItem;
    }

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the updated item, or null if not found
        return null;
    }

    public bool Delete(string id)
    {
        // TODO: Students - Implement this method
        // Return true if deleted, false if not found
        return false;
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items
        return [];
    }

    public int ClearPurchased()
    {
        // TODO: Students - Implement this method
        // Return the count of removed items
        return 0;
    }

    public bool TogglePurchased(string id)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false if item not found
        return false;
    }

    public bool Reorder(IReadOnlyList<string> orderedIds)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false otherwise
        return false;
    }
}

