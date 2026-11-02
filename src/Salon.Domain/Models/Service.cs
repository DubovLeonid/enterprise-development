using Salon.Domain.Enums;

namespace Salon.Domain.Models;

/// <summary>Услуга, оказываемая салоном.</summary>
public class Service
{
    /// <summary>Уникальный идентификатор услуги.</summary>
    public required Guid Id { get; init; }

    /// <summary>Название услуги.</summary>
    public required string Name { get; init; }

    /// <summary>Категория услуги.</summary>
    public required ServiceCategory Category { get; init; }

    /// <summary>Стоимость услуги в рублях.</summary>
    public required decimal Price { get; init; }

    /// <summary>Длительность услуги.</summary>
    public required TimeSpan Duration { get; init; }

    /// <summary>Строковое представление услуги.</summary>
    public override string ToString() =>
        $"{Name} ({Category}), {Price:C}, {Duration.TotalMinutes} мин.";
}