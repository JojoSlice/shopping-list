namespace ShoppingList.Domain.Models;

public class ShoppingItem
{
    private string _name = string.Empty;
    private string? _notes;
    private int _quantity = 1;

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name
    {
        get => _name;
        set => _name = ValidateName(value);
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity = ValidateQuantity(value);
    }

    public string? Notes { get; set; }

    public bool IsPurchased { get; set; } = false;

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
        return name.Trim();
    }

    private static int ValidateQuantity(int quantity)
    {
        return quantity >= 1 ? quantity : 1;
    }
}
