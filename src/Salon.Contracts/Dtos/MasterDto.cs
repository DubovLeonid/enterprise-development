using System.ComponentModel.DataAnnotations;
using Salon.Domain.Enums;

namespace Salon.Contracts.Dtos;

/// <summary>
/// Информация о мастере салона.
/// </summary>
public class MasterDto
{
    /// <summary>Уникальный идентификатор мастера.</summary>
    public Guid Id { get; set; }

    /// <summary>ФИО мастера.</summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>Пол мастера.</summary>
    [Required]
    public Gender Gender { get; set; }

    /// <summary>Дата рождения мастера.</summary>
    [Required]
    public DateOnly BirthDate { get; set; }

    /// <summary>Серия и номер паспорта.</summary>
    [Required(ErrorMessage = "Паспорт обязателен")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Паспорт должен содержать 10 цифр")]
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>Специализация.</summary>
    [Required(ErrorMessage = "Специализация обязательна")]
    [MaxLength(100)]
    public string Specialization { get; set; } = string.Empty;

    /// <summary>Стаж работы.</summary>
    [Range(0, 60, ErrorMessage = "Стаж должен быть от 0 до 60 лет")]
    public int ExperienceYears { get; set; }
}

