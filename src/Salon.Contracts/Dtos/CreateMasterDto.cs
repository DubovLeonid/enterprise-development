using System.ComponentModel.DataAnnotations;
using Salon.Domain.Enums;

namespace Salon.Contracts.Dtos;

/// <summary>
/// Данные для создания или обновления мастера.
/// </summary>
public class CreateMasterDto
{
    /// <summary>ФИО мастера.</summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>Пол мастера.</summary>
    [Required(ErrorMessage = "Пол обязателен")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Недопустимое значение пола")] 
    public Gender Gender { get; set; }

    /// <summary>Дата рождения мастера.</summary>
    [Range(typeof(DateOnly), "1900-01-01", "2100-01-01", ErrorMessage = "Некорректная дата рождения")] 
    public DateOnly BirthDate { get; set; }

    /// <summary>Серия и номер паспорта.</summary>
    [Required(ErrorMessage = "Паспорт обязателен")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Паспорт должен содержать 10 цифр")]
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>Специализация.</summary>
    [Required(ErrorMessage = "Специализация обязательна")]
    [MaxLength(100, ErrorMessage = "Специализация не должна превышать 100 символов")]
    public string Specialization { get; set; } = string.Empty;

    /// <summary>Стаж работы.</summary>
    [Range(0, 60, ErrorMessage = "Стаж должен быть от 0 до 60 лет")]
    public int ExperienceYears { get; set; }
}