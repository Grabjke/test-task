using CSharpFunctionalExtensions;
using OrderService.SharedKernel;

namespace OrderService.Core.ValueObjects.Item;

public record Discount
{
    private Discount(decimal value, DiscountType type)
    {
        Value = value;
        Type = type;
    }

    public decimal Value { get; }
    public DiscountType Type { get; }

    public static Result<Discount, Error> Create(decimal value, DiscountType type)
    {
        if (type == DiscountType.Fixed && value <= 0)
            return Errors.General.ValueIsInvalid("Fixed discount должен быть > 0");

        if (type == DiscountType.Percent && value is <= 0 or > 100)
            return Errors.General.ValueIsInvalid("Percent discount должен быть от 1 до 100");

        return new Discount(value, type);
    }
}