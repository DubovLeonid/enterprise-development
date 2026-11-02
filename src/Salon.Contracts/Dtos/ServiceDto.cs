using System.ComponentModel.DataAnnotations;
using Salon.Domain.Enums;

namespace Salon.Contracts.Dtos;

/// <summary>
/// Информация об услуге.
/// </summary>
public class ServiceDto
{
    /// <summary>Уникальный идентификатор услуги.</summary>
    public Guid Id { get; set; }

    /// <summary>Название услуги.</summary>
    [Required(ErrorMessage = "Название обязательно")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>Категория услуги.</summary>
    [Required]
    public ServiceCategory Category { get; set; }

    /// <summary>Стоимость услуги.</summary>
    [Range(0.01, 100000, ErrorMessage = "Цена должна быть больше 0")]
    public decimal Price { get; set; }

    /// <summary>Длительность в минутах.</summary>
    [Range(1, 480, ErrorMessage = "Длительность от 1 до 480 минут")]
    public int DurationMinutes { get; set; }
}

