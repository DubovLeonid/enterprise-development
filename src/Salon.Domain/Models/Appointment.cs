namespace Salon.Domain.Models;

/// <summary>Запись клиента на услугу к мастеру.</summary>
public class Appointment
{
    /// <summary>Уникальный идентификатор записи.</summary>
    public required Guid Id { get; init; }

    /// <summary>Дата и время начала услуги.</summary>
    public required DateTime StartTime { get; init; }

    /// <summary>Идентификатор клиента.</summary>
    public required Guid ClientId { get; init; }

    /// <summary>Идентификатор мастера.</summary>
    public required Guid MasterId { get; init; }

    /// <summary>Идентификатор услуги.</summary>
    public required Guid ServiceId { get; init; }

    /// <summary>Признак постоянного клиента.</summary>
    public required bool IsRegular { get; init; }

    /// <summary>Клиент, записавшийся на услугу.</summary>
    public required Client Client { get; init; }

    /// <summary>Мастер, оказывающий услугу.</summary>
    public required Master Master { get; init; }

    /// <summary>Услуга, на которую записан клиент.</summary>
    public required Service Service { get; init; }

    /// <summary>Строковое представление записи.</summary>
    public override string ToString() =>
        $"{StartTime:dd.MM.yyyy HH:mm} | {Client.FullName} - {Master.FullName} | {Service.Name}";
}