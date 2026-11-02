using System.ComponentModel.DataAnnotations;

namespace Salon.Contracts.Dtos;

/// <summary>
/// Информация о записи клиента.
/// </summary>
public class AppointmentDto
{
    /// <summary>Уникальный идентификатор записи.</summary>
    public Guid Id { get; set; }

    /// <summary>Дата и время начала.</summary>
    [Required(ErrorMessage = "Дата и время обязательны")]
    public DateTime StartTime { get; set; }

    /// <summary>ID клиента.</summary>
    [Required(ErrorMessage = "ID клиента обязателен")]
    public Guid ClientId { get; set; }

    /// <summary>ID мастера.</summary>
    [Required(ErrorMessage = "ID мастера обязателен")]
    public Guid MasterId { get; set; }

    /// <summary>ID услуги.</summary>
    [Required(ErrorMessage = "ID услуги обязателен")]
    public Guid ServiceId { get; set; }

    /// <summary>Признак постоянного клиента.</summary>
    public bool IsRegular { get; set; }
}

