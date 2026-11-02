namespace Salon.Domain.Models;

/// <summary>Клиент салона красоты.</summary>
public class Client : Person
{
    /// <summary>Контактный телефон.</summary>
    public required string Phone { get; init; }

    /// <summary>Строковое представление клиента, включая телефон.</summary>
    public override string ToString() =>
        $"{base.ToString()}, тел.: {Phone}";
}