using Salon.Domain.Enums;

namespace Salon.Api.Dtos;

/// <summary>
/// Информация о мастере салона.
/// </summary>
public class MasterDto
{
    /// <summary>Уникальный идентификатор мастера.</summary>
    public Guid Id { get; set; }

    /// <summary>ФИО мастера.</summary>
    public string FullName { get; set; } = "";

    /// <summary>Пол мастера.</summary>
    public Gender Gender { get; set; }

    /// <summary>Дата рождения мастера.</summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>Серия и номер паспорта.</summary>
    public string PassportNumber { get; set; } = "";

    /// <summary>Специализация.</summary>
    public string Specialization { get; set; } = "";

    /// <summary>Стаж работы.</summary>
    public int ExperienceYears { get; set; }
}

/// <summary>
/// Данные для создания или обновления информации о мастере.
/// </summary>
public class CreateMasterDto
{
    /// <summary>ФИО мастера.</summary>
    public string FullName { get; set; } = "";

    /// <summary>Пол мастера.</summary>
    public Gender Gender { get; set; }

    /// <summary>Дата рождения мастера.</summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>Серия и номер паспорта.</summary>
    public string PassportNumber { get; set; } = "";

    /// <summary>Специализация.</summary>
    public string Specialization { get; set; } = "";

    /// <summary>Стаж работы.</summary>
    public int ExperienceYears { get; set; }
}