using CSharpFunctionalExtensions;
using OrderService.SharedKernel;

namespace OrderService.Core.ValueObjects.Item;

public record Money
{
    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    public static Result<Money, Error> Create(decimal amount, string currency)
    {
        if (amount < 0)
            return Errors.General.ValueIsInvalid("Amount");

        if (string.IsNullOrWhiteSpace(currency))
            return Errors.General.ValueIsInvalid("Currency");

        if (currency.Length != 3 || !currency.All(char.IsLetter))
            return Errors.General.ValueIsInvalid("Currency code should be 3 letters");

        return new Money(amount, currency.ToUpperInvariant());
    }
}