using System.ComponentModel.DataAnnotations;
using Salon.Domain.Enums;

namespace Salon.Contracts.Dtos;

/// <summary>
/// Информация о клиенте салона.
/// </summary>
public class ClientDto
{
    /// <summary>Уникальный идентификатор клиента.</summary>
    public Guid Id { get; set; }

    /// <summary>ФИО клиента.</summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>Контактный телефон.</summary>
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>Пол клиента.</summary>
    [Required(ErrorMessage = "Пол обязателен")]
    public Gender Gender { get; set; }

    /// <summary>Дата рождения.</summary>
    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateOnly BirthDate { get; set; }
}

