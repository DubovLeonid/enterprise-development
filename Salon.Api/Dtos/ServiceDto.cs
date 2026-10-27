using Salon.Domain.Enums;

namespace Salon.Api.Dtos;

/// <summary>
/// Информация об услуге салона.
/// </summary>
public class ServiceDto
{
    /// <summary>Уникальный идентификатор услуги.</summary>
    public Guid Id { get; set; }

    /// <summary>Название услуги.</summary>
    public string Name { get; set; } = "";

    /// <summary>Категория услуги.</summary>
    public ServiceCategory Category { get; set; }

    /// <summary>Стоимость услуги в рублях.</summary>
    public decimal Price { get; set; }

    /// <summary>Длительность услуги в минутах.</summary>
    public int DurationMinutes { get; set; }
}

/// <summary>
/// Данные для создания или обновления информации об услуге.
/// </summary>
public class CreateServiceDto
{
    /// <summary>Название услуги.</summary>
    public string Name { get; set; } = "";

    /// <summary>Категория услуги.</summary>
    public ServiceCategory Category { get; set; }

    /// <summary>Стоимость услуги в рублях.</summary>
    public decimal Price { get; set; }

    /// <summary>Длительность услуги в минутах.</summary>
    public int DurationMinutes { get; set; }
}