using System;

namespace Salon.Models;

/// <summary>Запись клиента на услугу к мастеруИ.</summary>
public class Appointment
{
    /// <summary>Уникальный идентификатор записи.</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Дата и время начала услуги.</summary>
    public DateTime DateTime { get; set; }

    /// <summary>Идентификатор клиента.</summary>
    public Guid ClientId { get; set; }

    /// <summary>Идентификатор мастера.</summary>
    public Guid MasterId { get; set; }

    /// <summary>Идентификатор услуги.</summary>
    public Guid ServiceId { get; set; }

    /// <summary>Признак постоянного клиента.</summary>
    public bool IsRegular { get; set; }

    /// <summary>Клиент, записавшийся на услугу.</summary>
    public Client Client { get; set; } = null!;

    /// <summary>Мастер, оказывающий услугу.</summary>
    public Master Master { get; set; } = null!;

    /// <summary>Услуга, на которую записан клиент.</summary>
    public Service Service { get; set; } = null!;

    /// <summary>Строковое представление записи.</summary>
    public override string ToString() =>
        $"{DateTime:dd.MM.yyyy HH:mm} | {Client.FullName} - {Master.FullName} | {Service.Name}";
}