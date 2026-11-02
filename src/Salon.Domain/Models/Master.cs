namespace Salon.Domain.Models;

/// <summary>Мастер салона красоты.</summary>
public class Master : Person
{
    /// <summary>Серия и номер паспорта.</summary>
    public required string PassportNumber { get; init; }

    /// <summary>Специализация.</summary>
    public required string Specialization { get; init; }

    /// <summary>Стаж работы в годах.</summary>
    public required int ExperienceYears { get; init; }

    /// <summary>Строковое представление мастера, включая паспорт и стаж.</summary>
    public override string ToString() =>
        $"{base.ToString()}, паспорт: {PassportNumber}, " +
        $"спец.: {Specialization}, стаж: {ExperienceYears} лет";
}