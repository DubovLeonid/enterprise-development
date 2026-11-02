using Salon.Domain.Enums;

namespace Salon.Domain.Models;

/// <summary>
/// Базовый класс для клиента и мастера салона красоты.
/// </summary>
public abstract class Person
{
    /// <summary>Уникальный идентификатор персоны.</summary>
    public required Guid Id { get; init; }

    /// <summary>Полное имя.</summary>
    public required string FullName { get; init; }

    /// <summary>Пол.</summary>
    public required Gender Gender { get; init; }

    /// <summary>Дата рождения.</summary>
    public required DateOnly BirthDate { get; init; }

    /// <summary>Строковое представление персоны.</summary>
    public override string ToString() =>
        $"{FullName} ({Gender}, {BirthDate:dd.MM.yyyy})";
}