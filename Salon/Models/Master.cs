namespace Salon.Models;

/// <summary>Мастер салона красоты.</summary>
public class Master : Person
{
    /// <summary>Серия и номер паспорта.</summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>Специализация.</summary>
    public string Specialization { get; set; } = string.Empty;

    /// <summary>Стаж работы в годах.</summary>
    public int ExperienceYears { get; set; }

    /// <summary>Строковое представление мастера, включая паспорт и стаж.</summary>
    public override string ToString() =>
        $"{base.ToString()}, паспорт: {PassportNumber}, " +
        $"спец.: {Specialization}, стаж: {ExperienceYears} лет";
}