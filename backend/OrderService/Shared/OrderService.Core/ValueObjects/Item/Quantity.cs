using CSharpFunctionalExtensions;
using OrderService.SharedKernel;

namespace OrderService.Core.ValueObjects.Item;

public record Quantity
{
    private const int MAX_HIGH_QUANTITY = 300;
    private Quantity(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Quantity, Error> Create(int value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("Quantity должен быть положительным числом");

        if (value > MAX_HIGH_QUANTITY)
            return Errors.General.ValueIsInvalid("Quantity слишком большой");

        return new Quantity(value);
    }
}