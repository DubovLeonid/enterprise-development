using System;

namespace Salon.Models;

/// <summary>Услуга, оказываемая салоном.</summary>
public class Service
{
    /// <summary>Уникальный идентификатор услуги.</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Название услуги.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Категория услуги.</summary>
    public ServiceCategory Category { get; set; }

    /// <summary>Стоимость услуги в рублях.</summary>
    public decimal Price { get; set; }

    /// <summary>Длительность услуги.</summary>
    public TimeSpan Duration { get; set; }

    /// <summary>Строковое представление услуги.</summary>
    public override string ToString() =>
        $"{Name} ({Category}), {Price:C}, {Duration.TotalMinutes} мин.";
}