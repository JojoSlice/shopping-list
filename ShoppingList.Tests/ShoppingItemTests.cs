using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;

namespace ShoppingList.Tests;

/// <summary>
/// Example unit tests for the ShoppingItem domain model.
/// These tests demonstrate best practices for unit testing including:
/// - Clear, descriptive test names following the pattern: Method_Scenario_ExpectedBehavior
/// - Arrange-Act-Assert structure for test organization
/// - Testing both happy paths and edge cases
/// - Comprehensive coverage of validation rules
/// </summary>
public class ShoppingItemTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldSetDefaultValues()
    {
        // Arrange & Act
        var item = new ShoppingItem();

        // Assert
        Assert.NotNull(item.Id);
        Assert.NotEmpty(item.Id);
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal(1, item.Quantity);
        Assert.Null(item.Notes);
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void Constructor_ShouldGenerateUniqueId()
    {
        // Arrange & Act
        var item1 = new ShoppingItem();
        var item2 = new ShoppingItem();

        // Assert
        Assert.NotEqual(item1.Id, item2.Id);
    }

    #endregion

    #region Name Validation Tests

    [Fact]
    public void Name_WithValidValue_ShouldSetName()
    {
        // Arrange
        var item = new ShoppingItem();
        var expectedName = "Milk";

        // Act
        item.Name = expectedName;

        // Assert
        Assert.Equal(expectedName, item.Name);
    }

    [Fact]
    public void Name_WithLeadingAndTrailingWhitespace_ShouldTrimValue()
    {
        // Arrange
        var item = new ShoppingItem();
        var nameWithWhitespace = "  Bread  ";
        var expectedName = "Bread";

        // Act
        item.Name = nameWithWhitespace;

        // Assert
        Assert.Equal(expectedName, item.Name);
    }

    [Fact]
    public void Name_WithNull_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = null!);
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Name_WithEmptyString_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = string.Empty);
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
    }

    [Fact]
    public void Name_WithWhitespaceOnly_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = "   ");
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
    }

    #endregion

    #region Quantity Validation Tests

    [Fact]
    public void Quantity_WithValidPositiveValue_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();
        var expectedQuantity = 5;

        // Act
        item.Quantity = expectedQuantity;

        // Assert
        Assert.Equal(expectedQuantity, item.Quantity);
    }

    [Fact]
    public void Quantity_WithOne_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = 1;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithZero_ShouldDefaultToOne()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = 0;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithNegativeValue_ShouldDefaultToOne()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = -5;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithLargeValue_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();
        var largeQuantity = 1000;

        // Act
        item.Quantity = largeQuantity;

        // Assert
        Assert.Equal(largeQuantity, item.Quantity);
    }

    #endregion

    #region IsPurchased Tests

    [Fact]
    public void IsPurchased_DefaultValue_ShouldBeFalse()
    {
        // Arrange & Act
        var item = new ShoppingItem();

        // Assert
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void IsPurchased_CanBeSetToTrue()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.IsPurchased = true;

        // Assert
        Assert.True(item.IsPurchased);
    }

    [Fact]
    public void IsPurchased_CanBeToggled()
    {
        // Arrange
        var item = new ShoppingItem();
        var originalValue = item.IsPurchased;

        // Act
        item.IsPurchased = !item.IsPurchased;
        var toggledValue = item.IsPurchased;
        item.IsPurchased = !item.IsPurchased;
        var toggledBackValue = item.IsPurchased;

        // Assert
        Assert.False(originalValue);
        Assert.True(toggledValue);
        Assert.False(toggledBackValue);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void ShoppingItem_WithAllValidProperties_ShouldCreateSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var name = "Apples";
        var quantity = 10;
        var notes = "Pink Lady";

        // Act
        var item = new ShoppingItem
        {
            Id = id,
            Name = name,
            Quantity = quantity,
            Notes = notes,
        };

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(name, item.Name);
        Assert.Equal(quantity, item.Quantity);
        Assert.Equal(notes, item.Notes);
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void ShoppingItem_WithAutoTrimming_ShouldTrimName()
    {
        // Arrange & Act
        var item = new ShoppingItem { Name = "  Milk  ", Notes = "  Organic  " };

        // Assert
        Assert.Equal("Milk", item.Name);
        Assert.Equal("  Organic  ", item.Notes); // Notes are not trimmed
    }

    #endregion


    [Fact]
    public void Add_ShouldReturnItem()
    {
        var expected = new ShoppingItem
        {
            Name = "itemTest",
            Quantity = 1,
            Notes = "test item",
        };

        var sut = new ShoppingListService();
        var actual = sut.Add(expected.Name, expected.Quantity, expected.Notes);

        Assert.NotEmpty(actual.Name);

        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Quantity, actual.Quantity);
        Assert.Equal(expected.Notes, actual.Notes);
    }
}
