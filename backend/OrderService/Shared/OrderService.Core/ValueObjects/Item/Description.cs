using CSharpFunctionalExtensions;
using OrderService.SharedKernel;

namespace OrderService.Core.ValueObjects.Item;

public record Description
{
    private Description(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        return new Description(value);
    }
}