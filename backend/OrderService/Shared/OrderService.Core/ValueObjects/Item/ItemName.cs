using CSharpFunctionalExtensions;
using OrderService.SharedKernel;

namespace OrderService.Core.ValueObjects.Item;

public record ItemName
{
    private ItemName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ItemName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("ItemName");

        if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("ItemName length");
        
        return new ItemName(value.Trim());
    }
}